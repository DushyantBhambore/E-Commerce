import { TestBed } from '@angular/core/testing';

import { ForgoTService } from './forgo-t.service';

describe('ForgoTService', () => {
  let service: ForgoTService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForgoTService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
