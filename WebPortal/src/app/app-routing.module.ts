import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TimeManagementComponent } from './components/time-management/time-management.component';


const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'timeManagement', component: TimeManagementComponent },

  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
