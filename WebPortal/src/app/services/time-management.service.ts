import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { ApiService } from './api.service';
import { WeekDay } from '../models/Day/WeekDay';
import { TimeResults } from '../models/Results/TimeResults';
import { Moment } from 'moment';
import { TimeData } from '../models/Day/TimeData';

@Injectable({
  providedIn: 'root',
})
export class TimeManagementService {
  public api_url_tag: string;

  constructor(private apiService: ApiService) {
    this.api_url_tag = 'TimeManagement';
  }

  GetFlexiCalculations(weekDayList: WeekDay[]): Promise<HttpResponse<TimeResults>>{
    return this.apiService.post<TimeResults>(`${this.api_url_tag}/CalculateFlexiTimes`, weekDayList);
  }

  GetExistingTimeData(weekCommencing: string): Promise<HttpResponse<WeekDay[]>>{
    return this.apiService.get<WeekDay[]>(`${this.api_url_tag}/GetDaysOfTheWeekByCommencingDate?commencingDate=${weekCommencing}`);
  }

  SaveWeek(weekData: TimeData[], weekCommencing: string)
  {
    return this.apiService.post<TimeData[]>(`${this.api_url_tag}/SaveOrUpdateTimeData?dateCommencing=${weekCommencing}`, weekData);
  }
}
