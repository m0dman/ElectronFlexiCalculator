import { Component, OnInit, Input, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-page-link',
  templateUrl: './page-link.component.html',
  styleUrls: ['./page-link.component.scss']
})
export class PageLinkComponent implements OnInit {
  @Input()
  pageName: string;
  @Input()
  pageLink: string;
  @Input()
  pageIcon: string;
  @Input()
  isDropdown: boolean = false;

  isAuthorized: boolean = true;

  opened: boolean;
    showMenu = false;

  constructor() {}

  ngOnInit() {
  }
}
