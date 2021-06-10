import { IWeekDay } from '../interfaces/Day/IWeekDay';
import { Moment } from 'moment';

export class WeekDay implements IWeekDay {
    id: string;
    dayOfTheWeek: string;
    startTime: Moment;
    endTime: Moment;
    lunchLength: Moment;

    constructor(id: string, dayOfTheWeek: string, startTime: Moment, endTime: Moment, lunchLength: Moment) {
        this.id = id
        this.dayOfTheWeek = dayOfTheWeek;
        this.startTime = startTime;
        this.endTime = endTime;
        this.lunchLength = lunchLength;
    }

    getTypes() {
        return {}
    }
}
