/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { KednoGridService } from './kedno-grid.service';

describe('Service: KednoGrid', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [KednoGridService]
    });
  });

  it('should ...', inject([KednoGridService], (service: KednoGridService) => {
    expect(service).toBeTruthy();
  }));
});
