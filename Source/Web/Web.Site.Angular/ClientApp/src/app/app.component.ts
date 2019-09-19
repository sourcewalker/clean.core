import { Component } from '@angular/core';
import { GraphqlService } from './service/graphql.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private service: GraphqlService) {
  }

  ngOnInit(): void {
    this.service.getSiteByCulture('en-GB');
  }
  title = 'angulargraphqlclient';
}
