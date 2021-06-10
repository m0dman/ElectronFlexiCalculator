import * as moment from 'moment';

export class Utilities {
  //Get the name of a variable name from the given interface
  public static NameOf<A, B = {}>(): <A>(name: keyof A, name2?: keyof B) => any {
    return <A>(name: keyof A, name2?: keyof B) => {
      if (name2 !== undefined) {
        return `${name}.${name2}`;
      } else return name;
    };
  }

  //Formats the passed through date to local client time.
  static getUTCDate = (date: Date): Date => {
    let utcOffset = moment(date).utcOffset();
    return moment
      .utc(date)
      .utcOffset(utcOffset)
      .toDate();
  };

  //Formats the passed through date to local client time.
  static getDateAndTime = (date: Date): string => {
    let utcOffset = moment(date).utcOffset();
    return moment
      .utc(date)
      .utcOffset(utcOffset)
      .format('MMM-DD-YYYY HH:mm');
  };

  //Get the time in minutes between the two given dates.
  static getDifferenceInMinutes(date1: Date, date2: Date): number {
    let difference = (date2.getTime() - date1.getTime()) / 1000;
    difference /= 60;
    return Math.abs(difference);
  }
}
