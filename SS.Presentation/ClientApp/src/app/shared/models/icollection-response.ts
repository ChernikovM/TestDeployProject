import {IResponseBase} from "./i-response-base";
import {IPageInfoResponse} from "./ipage-info-response";
import {IFilterInfo} from "./ifilter-info";

export interface ICollectionResponse extends IResponseBase {
  pageInfo: IPageInfoResponse,
  filters: IFilterInfo[],
  data: any[]
}
