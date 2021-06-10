import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TimeManagementAddComponent } from './time-management-add.component';


describe('TimeManagementAddComponent', () => {
  let component: TimeManagementAddComponent;
  let fixture: ComponentFixture<TimeManagementAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeManagementAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeManagementAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
