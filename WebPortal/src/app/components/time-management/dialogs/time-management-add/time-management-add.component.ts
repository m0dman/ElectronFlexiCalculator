import { IDialogData } from 'src/app/models/interfaces/Shared/dialogData';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { TimeManagementComponent } from '../../time-management.component';
import { IDay } from 'src/app/models/interfaces/Day/IDay';

@Component({
  selector: 'app-time-management-add',
  templateUrl: './time-management-add.component.html',
  styleUrls: ['./time-management-add.component.scss']
})
export class TimeManagementAddComponent {
  constructor(public timeManagementDialogRef: MatDialogRef<TimeManagementComponent>, @Inject(MAT_DIALOG_DATA) public data: IDialogData<IDay>) {
  }

  form: FormGroup;

  get startTime() {
    return this.form.get("startTime");
  }
  
  get endTime() {
    return this.form.get("endTime");
  }

  get lunchLength() {
    return this.form.get("lunchLength");
  }

  ngOnInit(){
    this.form = new FormGroup({
      lunchLength: new FormControl(""),
      startTime: new FormControl(""),
      endTime: new FormControl("")
    });
  }

  //Close the dialog box if cancel is clicked
  onCancelClick(): void {
    this.timeManagementDialogRef.close();
  }

  onTimeChange(): void {
    this.data.value.startTime = this.startTime.value;
    this.data.value.endTime = this.endTime.value;
    this.data.value.lunchLength = this.lunchLength.value;
  }
}
