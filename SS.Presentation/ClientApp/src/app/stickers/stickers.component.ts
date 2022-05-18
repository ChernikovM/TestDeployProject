import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {Label} from "../shared/models/label";
import {Select, Store} from "@ngxs/store";
import {StickersActions} from "../shared/state-manager/stickers-collection-view/stickers.actions";
import {IPageInfoResponse} from "../shared/models/ipage-info-response";
import {StickersState} from "../shared/state-manager/stickers-collection-view/stickers.state";
import {StickerPack} from "../shared/models/sticker-pack";

@Component({
  selector: 'app-stickers',
  templateUrl: './stickers.component.html',
  styleUrls: ['./stickers.component.css']
})
export class StickersComponent implements OnInit {

  @Select(StickersState.getLabels) labels$!: Observable<Label[]>;
  @Select(StickersState.getStickerPacks) stickerPacks$!: Observable<StickerPack[]>;
  @Select(StickersState.getPageInfo) pageInfo$!: Observable<IPageInfoResponse>;

  constructor(
    private _store: Store,
  ) { }

  ngOnInit(): void {
    this._store.dispatch(new StickersActions.GetLabelsCollection())
      .subscribe(_ => this._store.dispatch(new StickersActions.GetStickersCollection()));
  }

  loadLabels(){
    this._store.dispatch(new StickersActions.GetLabelsCollection());
  }

  loadStickers(){
    this._store.dispatch(new StickersActions.GetStickersCollection());
  }

  updateView(){
    this.loadStickers();
  }
}
