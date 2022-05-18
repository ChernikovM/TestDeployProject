import {Label} from "../../models/label";
import {ICollectionResponse} from "../../models/icollection-response";
import {Action, Selector, State, StateContext} from "@ngxs/store";
import {Injectable} from "@angular/core";
import {LabelsService} from "../../services/labels.service";
import {DataCollectionRequestModel} from "../../models/DataCollectionRequestModel/data-collection-request-model";
import {catchError, tap} from "rxjs/operators";
import {IResponseBase} from "../../models/i-response-base";
import {StickersActions} from "./stickers.actions";
import { IFilterInfo } from "../../models/ifilter-info";
import {environment} from "../../../../environments/environment";
import {StickersService} from "../../services/stickers.service";
import {StickerPack} from "../../models/sticker-pack";
import {IPageInfoResponse} from "../../models/ipage-info-response";

export class StickersStateModel {
  public responseModel?: ICollectionResponse;
  public labels?: Label[];
  public stickerPacks?: StickerPack[];
  public page?: number;
  public pageSize?: number;
  public filters?: IFilterInfo[];
  public labelIds?: number[];
  public pageInfoResponse?: IPageInfoResponse;
}

const defaults = {
  responseModel: undefined,
  labels: [],
  stickerPacks: [],
  page: 1,
  pageSize: environment.defaultPageSize,
  filters: [],
  labelIds: [],
  pageInfoResponse: {
    pageSize: undefined,
    currentPage: undefined,
    fullCollectionSize: undefined
  }
};

@State<StickersStateModel>({
  name: 'stickers',
  defaults
})
@Injectable()
export class StickersState {

  constructor(
    private labelsService: LabelsService,
    private stickersService: StickersService) {
  }

  @Selector()
  static getLabels(state: StickersStateModel){
    return state.labels;
  }

  @Selector()
  static getStickerPacks(state: StickersStateModel){
    return state.stickerPacks;
  }

  @Selector()
  static getPageInfo(state: StickersStateModel){
    return state.pageInfoResponse;
  }

  @Selector()
  static getFilters(state: StickersStateModel){
    return state.filters;
  }

  @Selector()
  static getPageSize(state: StickersStateModel){
    return state.pageSize;
  }


  @Action(StickersActions.GetLabelsCollection)
  getLabels(context: StateContext<StickersStateModel>){
    const currentState = context.getState();

    return this.labelsService
      .getLabels(new DataCollectionRequestModel())
      .pipe(
        tap((result: ICollectionResponse) => {
          context.setState({
            ...currentState,
            // responseModel: result,
            labels: result.data
          });

          console.log(context.getState());

        }),
        catchError(async (result: IResponseBase) => {
          if(result?.errors?.length !== undefined){
            for(let i = 0; i < result.errors.length; ++i){
              console.log(result.errors[i]);
            }
          }
        })
      );
  }

  @Action(StickersActions.GetStickersCollection)
  getStickerPacks(context: StateContext<StickersStateModel>){
    const currentState = context.getState();
    console.log(currentState);
    console.log(currentState.labels?.length);

    let request = new DataCollectionRequestModel();
    request.page = currentState.page;
    request.pageSize = currentState.pageSize;
    request.filters = currentState.filters;
    request.labelIds = currentState.labelIds;

    return this.stickersService
      .getStickerPacks(request)
      .pipe(
        tap((result: ICollectionResponse) => {
          context.setState({
            ...currentState,
            // responseModel: result,
            stickerPacks: result.data,
            filters: result.filters,
            pageInfoResponse: result.pageInfo,
            pageSize: result.pageInfo.pageSize
          });
        }),
        catchError(async (result: IResponseBase) => {
          if(result?.errors?.length !== undefined){
            for(let i = 0; i < result.errors.length; ++i){
              console.log(result.errors[i]);
            }
          }
        })
      );
  }

  @Action(StickersActions.SetCurrentPage)
  setCurrentPage(context: StateContext<StickersStateModel>, { newPage }: StickersActions.SetCurrentPage){
    const state = context.getState();
    context.setState({
      ...state,
      page: newPage
    });
  }

  @Action(StickersActions.SetPageSize)
  setPageSize(context: StateContext<StickersStateModel>, { newSize }: StickersActions.SetPageSize){
    const state = context.getState();
    context.setState({
      ...state,
      pageSize: newSize
    });
  }
}
