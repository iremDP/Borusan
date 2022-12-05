import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListeleSiparisComponent } from './listele-siparis.component';

describe('ListeleSiparisComponent', () => {
  let component: ListeleSiparisComponent;
  let fixture: ComponentFixture<ListeleSiparisComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListeleSiparisComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListeleSiparisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
