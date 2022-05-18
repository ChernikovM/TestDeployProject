import {IDataCollectionRequestModel} from "./idata-collection-request-model";
import {IFilterInfo} from "../ifilter-info";

export class DataCollectionRequestModel{
  public filters?: IFilterInfo[];
  public labelIds?: number[];
  public page?: number;
  public pageSize?: number;
}
