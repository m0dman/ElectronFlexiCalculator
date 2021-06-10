import { IDialogData } from '../interfaces/Shared/dialogData';

export class DialogData<T, O> implements IDialogData<T, O> {
  value: T;
  tableData?: T[];
  dialogOptions?: O[];

  constructor(value: T, tableData?: T[], dialogOptions?: O[]) {
    this.value = value;
    if (tableData !== undefined)
      this.tableData = tableData;
    if (dialogOptions !== undefined)
      this.dialogOptions = dialogOptions;
  }
}
