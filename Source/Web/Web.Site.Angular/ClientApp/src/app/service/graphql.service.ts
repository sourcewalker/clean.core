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
      query: gql`query getOwner($siteCult: ID!){
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
}
