import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";
import {ICollectionResponse} from "../models/icollection-response";
import {environment} from "../../../environments/environment";
import {DataCollectionRequestModel} from "../models/DataCollectionRequestModel/data-collection-request-model";

@Injectable({
  providedIn: 'root'
})
export class LabelsService {

  private apiUrl: string = environment.apiUrl + '/labels';

  constructor(private http: HttpClient) { }

  getLabels(dataCollectionRequestModel: DataCollectionRequestModel): Observable<ICollectionResponse> {
    return this.http.post<ICollectionResponse>(`${this.apiUrl}/get`, dataCollectionRequestModel);
  }
}
