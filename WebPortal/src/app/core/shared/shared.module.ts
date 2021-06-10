import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from 'src/app/components/loading/loading.component';
import { MaterialUiModule } from '../material-ui/material-ui.module';



@NgModule({
  declarations: [LoadingComponent],
  imports: [
    CommonModule,
    MaterialUiModule
  ],
  exports:[LoadingComponent]
})
export class SharedModule { }
