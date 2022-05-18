import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import {NgxsModule} from "@ngxs/store";
import {states} from "./state-manager/states";
import {NgxsReduxDevtoolsPluginModule} from "@ngxs/devtools-plugin";
import {PaginationComponent} from "./components/pagination/pagination.component";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import { PageSizeDropwodnComponent } from './components/page-size-dropwodn/page-size-dropwodn.component';

@NgModule({
  declarations: [
    PaginationComponent,
    PageSizeDropwodnComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    NgxsModule.forRoot(states),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NgbModule,
  ],
  exports: [
    PaginationComponent,
    PageSizeDropwodnComponent,
  ]
})
export class SharedModule { }
