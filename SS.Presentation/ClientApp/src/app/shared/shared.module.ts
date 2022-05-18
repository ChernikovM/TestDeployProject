import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import {NgxsModule} from "@ngxs/store";
import {states} from "./state-manager/states";
import {NgxsReduxDevtoolsPluginModule} from "@ngxs/devtools-plugin";
import {PaginationComponent} from "./components/pagination/pagination.component";

@NgModule({
  declarations: [
    PaginationComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    NgxsModule.forRoot(states),
    NgxsReduxDevtoolsPluginModule.forRoot()
  ],
  exports: [
    PaginationComponent
  ]
})
export class SharedModule { }
