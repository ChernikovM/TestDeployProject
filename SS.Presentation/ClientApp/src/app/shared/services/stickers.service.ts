import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {DataCollectionRequestModel} from "../models/DataCollectionRequestModel/data-collection-request-model";
import {Observable} from "rxjs";
import {ICollectionResponse} from "../models/icollection-response";

@Injectable({
  providedIn: 'root'
})
export class StickersService {

  private apiUrl: string = environment.apiUrl + '/StickerPacks';

  constructor(private http: HttpClient) { }

  getStickerPacks(dataCollectionRequestModel: DataCollectionRequestModel): Observable<ICollectionResponse> {
    return this.http.post<ICollectionResponse>(`${this.apiUrl}/get`, dataCollectionRequestModel);
  }
}
