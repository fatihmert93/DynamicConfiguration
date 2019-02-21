import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from "../model/user.model";
import { Configuration } from '../model/configuration.model';
import { ResponseModel } from '../model/responseModel';

@Injectable()
export class ConfigurationService {
  constructor(private http: HttpClient) { }
  baseUrl: string = 'http://localhost:5000/api/configurations';

  getList(searchModel: String) {
    return this.http.get<ResponseModel>(this.baseUrl + "/list?searchModel=" + searchModel);
  }

  getById(id: string) {
    return this.http.get<ResponseModel>(this.baseUrl + '/' + id);
  }

  create(config: Configuration) {
    return this.http.post(this.baseUrl, config);
  }

  update(config: Configuration) {
    return this.http.post(this.baseUrl, config);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + '?_id=' + id);
  }
}
