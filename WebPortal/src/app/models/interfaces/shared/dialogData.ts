export interface IDialogData<T, S = {}> {
  value: T;
  tableData?: T[];
  dialogOptions?: S[];
}
