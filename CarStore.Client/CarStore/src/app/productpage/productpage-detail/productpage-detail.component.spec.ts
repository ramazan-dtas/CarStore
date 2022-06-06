import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductpageDetailComponent } from './productpage-detail.component';

describe('ProductpageDetailComponent', () => {
  let component: ProductpageDetailComponent;
  let fixture: ComponentFixture<ProductpageDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductpageDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductpageDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
