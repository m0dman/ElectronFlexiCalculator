import { Injectable, isDevMode } from '@angular/core'
import { HttpHeaders, HttpClient, HttpBackend } from '@angular/common/http'
import { IAppConfig } from '../models/configurations/appconfig'

@Injectable()
export class SettingsProvider {
  static appConfig: IAppConfig;
  private http: HttpClient;

  constructor(private handler: HttpBackend) {
    this.http = new HttpClient(this.handler);
    this.load();
  }
  // Http Options
  httpOptions = {
      headers: new HttpHeaders({
          'Content-Type': 'application/json',
      }),
  }

  load() {
    let jsonFile = `assets/config.json`
    if (!isDevMode()) {
        //console.log('SettingsProvider !isDevMode')
        jsonFile = `assets/config.json`;
    }
    return new Promise<IAppConfig>((resolve, reject) => {
      this.http.get(jsonFile).subscribe((response: IAppConfig) => {
        SettingsProvider.appConfig = {
          env: response.env,
          apiServer: response.apiServer
        };
        resolve();
      });
    })
  }
}
