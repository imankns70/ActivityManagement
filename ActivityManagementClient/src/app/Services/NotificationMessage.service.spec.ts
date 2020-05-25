/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { NotificationMessageService } from './NotificationMessage.service';

describe('Service: Common', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NotificationMessageService]
    });
  });

  it('should ...', inject([NotificationMessageService], (service: NotificationMessageService) => {
    expect(service).toBeTruthy();
  }));
});
