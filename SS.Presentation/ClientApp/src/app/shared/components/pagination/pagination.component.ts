import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Select, Store} from "@ngxs/store";
import {StickersActions} from "../../state-manager/stickers-collection-view/stickers.actions";
import {StickersState} from "../../state-manager/stickers-collection-view/stickers.state";
import {IPageInfoResponse} from "../../models/ipage-info-response";
import {Observable} from "rxjs";

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {

  @Select(StickersState.getPageInfo) pageInfo$!: Observable<IPageInfoResponse>;
  @Output() pageChanged = new EventEmitter();

  page?: number | undefined;
  collectionSize?: number | undefined;
  pageSize?: number | undefined;

  constructor(
    private _store: Store
  ) { }

  ngOnInit(): void {
    this.pageInfo$.subscribe(data => {
      this.page = data.currentPage;
      this.collectionSize = data.fullCollectionSize;
      this.pageSize = data.pageSize;
    });
  }

  onPageChanged(){
    if(this.page !== undefined && this.collectionSize !== undefined){
      if(this.page > 0 && this.page <= this.collectionSize){
        this.updatePage();
      }
    }
  }

  updatePage(){
    this._store.dispatch(new StickersActions.SetCurrentPage(this.page!));
    this.pageChanged.emit();
  }
}
