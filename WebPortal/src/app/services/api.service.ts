import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { SettingsProvider } from './settingsprovider';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  protected apiServer = SettingsProvider.appConfig.apiServer.url;
  constructor(private http: HttpClient) {}

  //Formats the errors that come from API requests and outputs them to the console.
  private formatErrors(error: any) {
    return throwError(error.error);
  }

  //Used to get data from the API.
  async get<T>(url: string): Promise<HttpResponse<T>> {
    return this.http
      .get<T>(`${this.apiServer}${url}`, {
        headers: new HttpHeaders(),
        observe: 'response',
      })
      .pipe()
      .toPromise();
  }

  async post<T>(url: string, body: Object = {}): Promise<HttpResponse<T>> {
    //console.log(`post url ${this.apiServer}${url} with panel: ${body}`);
    return this.http
      .post<T>(`${this.apiServer}${url}`, JSON.stringify(body), {
        headers: new HttpHeaders().set("Content-Type", "application/json"),
        observe: "response",
      })
      .pipe()
      .toPromise();
  }

  //Used to pass data to the API to be inserted into the database. Replace used on json to from _ when handling private variables.
  async put<T>(url: string, body: Object = {}): Promise<HttpResponse<T>> {
    return this.http
      .put<T>(`${this.apiServer}${url}`, JSON.stringify(body), {
        observe: 'response',
        headers: { "Content-Type": "application/json" }
      })
      .pipe()
      .toPromise();
  }

  //Used to pass an id to the API to then allow for deletion in the database.
  async delete<T>(url: string): Promise<HttpResponse<T>> {
    return this.http
      .delete<T>(`${this.apiServer}${url}`, {
        observe: 'response',
        headers: { "Content-Type": "application/json" }
      })
      .pipe()
      .toPromise();
  }
}
