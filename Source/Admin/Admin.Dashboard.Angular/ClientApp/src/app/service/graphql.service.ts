import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { HttpLink } from 'apollo-angular-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';
import gql from 'graphql-tag';
import { SiteType } from '../types/site.type';
 
@Injectable({
  providedIn: 'root'
})
export class GraphqlService {
  public sites: SiteType[];
  public site: SiteType;
  public createdSite: SiteType;
  public updatedSite: SiteType;
 
  constructor(private apollo: Apollo, httpLink: HttpLink) {
    apollo.create({
      link: httpLink.create({ uri: 'https://localhost:5001/graphql' }),
      cache: new InMemoryCache()
    })
  }

  public getSites = () => {
    this.apollo.query({
      query: gql`query getSites{
      sites{
        id,
        name,
        culture,
        domain,
        createdDate,
        modifiedDate
      }
    }`
    }).subscribe(result => {
      this.sites = result.data as SiteType[];
      console.log(this.sites);
    })
  }

  public getSiteByCulture = (culture) => {
    this.apollo.query({
      query: gql`query getSite($siteCult: ID!){
      site(siteCulture: $siteCult){
        id,
        name,
        culture,
        domain,
        createdDate,
        modifiedDate
      }
    }`,
      variables: { siteCult: culture }
    }).subscribe(result => {
      this.site = result.data as SiteType;
    })
  }

  public createSite = (siteToCreate: SiteType) => {
    this.apollo.mutate({
      mutation: gql`mutation($site: siteInput!){
        createSite(site: $site){
          id,
          name,
          culture,
          domain,
          createdDate,
          modifiedDate
        }
      }`,
      variables: { site: siteToCreate }
    }).subscribe(result => {
      this.createdSite = result.data as SiteType;
    })
  }

  public updateSite = (siteToUpdate: SiteType, id: string) => {
    this.apollo.mutate({
      mutation: gql`mutation($site: siteViewModel!, $siteId: ID!){
        updateSite(site: $site, siteId: $siteId){
          id,
          name,
          culture,
          domain,
          createdDate,
          modifiedDate
        }
      }`,
      variables: { site: siteToUpdate, siteId: id }
    }).subscribe(result => {
      this.updatedSite = result.data as SiteType;
    })
  }

  public deleteSite = (id: string) => {
    this.apollo.mutate({
      mutation: gql`mutation($siteId: ID!){
        deleteSite(siteId: $siteId)
       }`,
      variables: { siteId: id }
    }).subscribe(res => {
      console.log(res.data);
    })
  }
}
