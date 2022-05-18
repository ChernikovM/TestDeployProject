import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {StickersState} from "../../state-manager/stickers-collection-view/stickers.state";
import {Select, Store} from "@ngxs/store";
import {async, Observable} from "rxjs";
import {environment} from "../../../../environments/environment";
import {StickersActions} from "../../state-manager/stickers-collection-view/stickers.actions";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-page-size-dropwodn',
  templateUrl: './page-size-dropwodn.component.html',
  styleUrls: ['./page-size-dropwodn.component.css']
})
export class PageSizeDropwodnComponent implements OnInit {

  @Select(StickersState.getPageSize) pageSize$!: Observable<number>

  @Output() pageSizeChanged = new EventEmitter();

  currentSize: number = 0;

  availablePageSizes: number[];

  constructor(private _store: Store) {
    this.availablePageSizes = environment.availablePageSizes;
  }

  ngOnInit(): void {
    this.pageSize$.subscribe(data => this.currentSize = data);
  }

  onPageSizeChange(size: number){
    if(size == this.currentSize){
      return;
    }

    this._store.dispatch(new StickersActions.SetPageSize(size));
    this.pageSizeChanged.emit();
  }

}
