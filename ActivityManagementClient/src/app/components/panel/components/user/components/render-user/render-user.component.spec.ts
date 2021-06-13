/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RenderUserComponent } from './render-user.component';

describe('EditUserComponent', () => {
  let component: RenderUserComponent;
  let fixture: ComponentFixture<RenderUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RenderUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RenderUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
