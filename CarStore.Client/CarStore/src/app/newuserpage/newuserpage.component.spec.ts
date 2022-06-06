import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewuserpageComponent } from './newuserpage.component';

describe('NewuserpageComponent', () => {
  let component: NewuserpageComponent;
  let fixture: ComponentFixture<NewuserpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewuserpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewuserpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
