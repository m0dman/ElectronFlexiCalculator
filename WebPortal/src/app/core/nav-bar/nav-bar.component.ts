import { Component, OnInit, ViewChild } from '@angular/core';
import { isNullOrUndefined } from 'util';
import { Observable } from 'rxjs';
import { BreakpointObserver, BreakpointState, Breakpoints } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { ColorSchemeService } from 'src/app/services/color-scheme.service';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  //providing a default pageTitle incase no title is made available
  pageTitle: string = 'Randox IT Utility';
  visible: boolean = false;
    opened: boolean;
      showAdmin = false;
      showInstance = false;
  
  constructor(
    private breakpointObserver: BreakpointObserver,
    private colorSchemeService: ColorSchemeService
  ) {}

  @ViewChild('drawer', {static: true}) drawer: any;
  public selectedItem : string = '';
   public isHandset$: Observable<boolean> = this.breakpointObserver
     .observe(Breakpoints.Handset)
     .pipe(map((result: BreakpointState) => result.matches));

    public themes = [
    {
        name: 'dark',
        displayName: 'Dark',
        icon: 'brightness_3'
    },
    {
        name: 'light',
        displayName: 'Light',
        icon: 'wb_sunny'
    }
  ];

  ngOnInit() {
  }

  toggleAdminMenu() {
    this.showAdmin = !this.showAdmin;
 }
 toggleInstanceMenu() {
  this.showInstance = !this.showInstance;
 }
 setTheme(theme: string) {
  this.colorSchemeService.update(theme);
}
}
