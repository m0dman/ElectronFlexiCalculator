import { IDay } from '../interfaces/Day/IDay';
import { isNullOrUndefined } from 'util';

export class Day implements IDay {
    id: string;
    dayOfTheWeek: string;
    startTime: string;
    endTime: string;
    lunchLength: string;

    constructor(id: string, dayOfTheWeek: string, startTime?: string, endTime?: string, lunchLength?: string) {
        this.id = !isNullOrUndefined(id) ? id : "";
        this.dayOfTheWeek = dayOfTheWeek;
        this.startTime = !isNullOrUndefined(startTime) ? startTime : "08:40 AM";
        this.endTime = !isNullOrUndefined(endTime) ? endTime : "05:20 PM";
        this.lunchLength = !isNullOrUndefined(lunchLength) ? lunchLength : "00:40";
    }

    getTypes() {
        return {}
    }
}
