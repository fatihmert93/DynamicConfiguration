import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListConfigurationComponent } from './list-configuration.component';

describe('ListUserComponent', () => {
  let component: ListConfigurationComponent;
  let fixture: ComponentFixture<ListConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
