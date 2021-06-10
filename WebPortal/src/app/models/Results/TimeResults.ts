import { ITimeResults } from '../interfaces/Results/ITimeResults';
import { isNullOrUndefined } from 'util';

export class TimeResults implements ITimeResults {
    totalAmountOfFlexi: string;
    earliestCanLeave: string;
    can247Train: boolean;
    can347Train: boolean;
    can447Train: boolean;

    constructor(totalAmountOfFlexi: string, earliestCanLeave: string, can247Train?: boolean, can347Train?: boolean, can447Train?: boolean) {
        this.totalAmountOfFlexi = totalAmountOfFlexi;
        this.earliestCanLeave = earliestCanLeave;
        this.can247Train = !isNullOrUndefined(can247Train) ? can247Train : false;
        this.can347Train = !isNullOrUndefined(can347Train) ? can347Train : false;
        this.can447Train = !isNullOrUndefined(can447Train) ? can447Train : false;
    }


    getTypes() {
        return {}
    }
}
