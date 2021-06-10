import { ITimeData } from '../interfaces/Day/ITimeData';
import { Moment } from 'moment';

export class TimeData implements ITimeData {
    id: string;
    dayOfTheWeek: string;
    startTime: Moment;
    endTime: Moment;
    lunchLength: Moment;
    weekCommencing: Moment;

    constructor(id: string, dayOfTheWeek: string, startTime: Moment, endTime: Moment, lunchLength: Moment, weekCommencing: Moment) {
        this.id = id;
        this.dayOfTheWeek = dayOfTheWeek;
        this.startTime = startTime;
        this.endTime = endTime;
        this.lunchLength = lunchLength;
        this.weekCommencing = weekCommencing;
    }

    getTypes() {
        return {}
    }
}
