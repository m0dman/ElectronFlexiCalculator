import { Moment } from 'moment';

export interface ITimeData {
    dayOfTheWeek: string;
    startTime: Moment;
    endTime: Moment;
    lunchLength: Moment;
    weekCommencing: Moment;
}
