import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuncelleSiparisComponent } from './guncelle-siparis.component';

describe('GuncelleSiparisComponent', () => {
  let component: GuncelleSiparisComponent;
  let fixture: ComponentFixture<GuncelleSiparisComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GuncelleSiparisComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GuncelleSiparisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
