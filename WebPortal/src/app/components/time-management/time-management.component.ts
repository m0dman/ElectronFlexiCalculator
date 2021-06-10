import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { IDay } from 'src/app/models/interfaces/Day/IDay';
import { FormControl, FormGroup } from '@angular/forms';
import { TimeManagementAddComponent } from './dialogs/time-management-add/time-management-add.component';
import { IDialogData } from 'src/app/models/interfaces/Shared/dialogData';
import { DialogData } from 'src/app/models/shared/dialogData';
import { Day } from 'src/app/models/Day/Day';
import { Moment } from 'moment';
import * as moment from 'moment';
import { IWeekDay } from 'src/app/models/interfaces/Day/IWeekDay';
import { WeekDay } from 'src/app/models/Day/WeekDay';
import { TimeManagementService } from 'src/app/services/time-management.service'
import { TimeResults } from 'src/app/models/Results/TimeResults';
import { ITimeResults } from 'src/app/models/interfaces/Results/ITimeResults';
import { formatDate } from '@angular/common';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { TimeData } from 'src/app/models/Day/TimeData';
import { MatSnackBar } from '@angular/material';

const ELEMENT_DATA: IDay[] = [
  new Day("", "Monday"),
  new Day("", "Tuesday"),
  new Day("", "Wednesday"),
  new Day("", "Thursday"),
  new Day("", "Friday"),
];

@Component({
  selector: 'app-time-management',
  templateUrl: './time-management.component.html',
  styleUrls: ['./time-management.component.scss'],
})
export class TimeManagementComponent implements OnInit {
  displayedColumns: string[] = ['DayOfTheWeek', 'StartTime', 'EndTime', 'LunchLength', 'actions'];
  dataSource = new MatTableDataSource<IDay>();
  endResults = new TimeResults("Awaiting Calculation...", "Awaiting Calculation...", false, false, false); 

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  form: FormGroup;

  todayDate: Date = new Date();

  dateString: string;

  myFilter = (d: Date | null): boolean => {
    const day = (d || new Date()).getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6 && day !== 2 && day !== 3 && day !== 4 && day !== 5
  }

  constructor(private router: Router, public dialog: MatDialog, private timeManagementService: TimeManagementService, private snackBar: MatSnackBar) {
    this.dataSource = new MatTableDataSource<IDay>();
  }

  //Activates the loading symbol until deactivated
  isLoading = false;
  isDisabled = true;
  savingDisabled = true;

  ngOnInit() {
    // for (let i=0;i < ELEMENT_DATA.length;i++) {
    //   this.dataSource.data.push(ELEMENT_DATA[i])
    // }
    this.form = new FormGroup({
      lunchLength: new FormControl("")
    });
  }

  calculate() {
    let weekList: WeekDay[] = []
    for (let i=0;i < this.dataSource.data.length;i++) {
      const id: string = this.dataSource.data[0].id;
      const startTime: Moment = moment(this.dataSource.data[i].startTime, 'h:mm a');
      const endTime: Moment = moment(this.dataSource.data[i].endTime, 'h:mm a');
      const lunchLength: Moment = moment(this.dataSource.data[i].lunchLength, 'h:mm');
      let weekListDay: WeekDay = new WeekDay(id, this.dataSource.data[i].dayOfTheWeek, startTime, endTime, lunchLength);
      weekList.push(weekListDay);
    }

    this.timeManagementService.GetFlexiCalculations(weekList).then((result: HttpResponse<TimeResults>) => {
        this.endResults = new TimeResults(result.body.totalAmountOfFlexi, formatDate(result.body.earliestCanLeave, "hh:mm a", "en-EU"), result.body.can247Train, result.body.can347Train, result.body.can447Train )
      });
    }

  newDay(row: IDay) {
    let newDay: Day = new Day("", row.dayOfTheWeek);
    let dialogData: IDialogData<IDay> = new DialogData(newDay);

    //Opens the add dialog box
    const dialogRef = this.dialog.open(TimeManagementAddComponent, { data: dialogData });

    //Handles the close of the dialog box
    dialogRef.afterClosed().subscribe((result: IDialogData<IDay> | number) => {
      if (result !== -1 && result !== undefined) {
      let selectedDay = this.dataSource.data.find((e) => e.dayOfTheWeek === row.dayOfTheWeek);
      selectedDay.startTime = dialogData.value.startTime;
      selectedDay.endTime = dialogData.value.endTime;
      selectedDay.lunchLength = dialogData.value.lunchLength;
      }
      else if(result === -1){
        console.log('Cancelled!')
      }
      else
      {
        this.snackBar.open('Failed to add times', 'Error', {
          duration: 3000,
          panelClass: 'error-dialog',
        });
      }

    });
  }

  weekCommenceSelect(event: MatDatepickerInputEvent<Date>)
  {

    this.dateString  = event.value.toDateString();

    this.timeManagementService.GetExistingTimeData(this.dateString).then((result: HttpResponse<WeekDay[]>) => {

      this.dataSource.data = [];

      if (result.body.length > 1){
        this.savingDisabled = false;
      }
      else {
        this.savingDisabled = true;
      }

      for (var i=0;i < result.body.length;i++)
      {
        let ID = result.body[i].id;

        let DayOfTheWeek = result.body[i].dayOfTheWeek;

        let StartTimeString = result.body[i].startTime.toString()
        let StartTime =  formatDate(StartTimeString, "hh:mm a", "en-EU")

        let EndTimeString = result.body[i].endTime.toString()
        let EndTime =  formatDate(EndTimeString, "hh:mm a", "en-EU")

        let LunchLengthString = result.body[i].lunchLength.toString()
        let LunchLength =  formatDate(LunchLengthString, "HH:mm", "en-EU")

        let newDay = new Day(ID, DayOfTheWeek, StartTime, EndTime, LunchLength);

        this.dataSource.data.push(newDay);
      }
      this.dataSource.data = this.dataSource.data;
      this.isDisabled = false
    });
  }

  newWeek()
  {
    this.dataSource.data = [];
    this.savingDisabled = false;

    var newWeekList: IDay[] = [
      new Day("", "Monday"),
      new Day("", "Tuesday"),
      new Day("", "Wednesday"),
      new Day("", "Thursday"),
      new Day("", "Friday"),
    ];
    
    for (let i=0;i < newWeekList.length;i++) {
      this.dataSource.data.push(newWeekList[i]);
    }
    this.dataSource.data = this.dataSource.data;
  }

  saveWeek()
  {
    let weekList: TimeData[] = []
    for (let i=0;i < this.dataSource.data.length;i++) {
      const startTime: Moment = moment(this.dataSource.data[i].startTime, 'h:mm a');
      const endTime: Moment = moment(this.dataSource.data[i].endTime, 'h:mm a');
      const lunchLength: Moment = moment(this.dataSource.data[i].lunchLength, 'h:mm');
      const weekCommencing: Moment = moment(this.dateString, 'h:mm');
      let weekListDay: TimeData = new TimeData(this.dataSource.data[i].id ,this.dataSource.data[i].dayOfTheWeek, startTime, endTime, lunchLength, weekCommencing);
      weekList.push(weekListDay);
    }
      this.timeManagementService.SaveWeek(weekList ,this.dateString).then((result: HttpResponse<TimeData[]>) => {
        console.log("Saved/Updated Successfully!");
      });
    }
  }