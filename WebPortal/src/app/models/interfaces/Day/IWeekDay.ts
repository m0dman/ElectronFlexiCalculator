import { Moment } from 'moment';

export interface IWeekDay {
    id: string;
    dayOfTheWeek: string;
    startTime: Moment;
    endTime: Moment;
    lunchLength: Moment;
}
