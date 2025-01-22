import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { HackerNewsService } from './hacker-news.service';

describe('HackerNewsService', () => {
  let service: HackerNewsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        HackerNewsService
      ],
});
    service = TestBed.inject(HackerNewsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch top story IDs', () => {
    const mockStoryIds = [1, 2, 3, 4, 5];

    service.getTopItems().subscribe((ids) => {
      expect(ids).toEqual(mockStoryIds);
    });

    const req = httpMock.expectOne('https://localhost:7036/api/HackerNews/');
    expect(req.request.method).toBe('GET');
    req.flush(mockStoryIds);
  });

});
