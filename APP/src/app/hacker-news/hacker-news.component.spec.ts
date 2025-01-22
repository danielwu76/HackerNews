import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';

import { HackerNewsComponent } from './hacker-news.component';
import { HackerNewsService } from './hacker-news.service';


describe('HackerNewsComponent', () => {
  let component: HackerNewsComponent;
  let fixture: ComponentFixture<HackerNewsComponent>;
  let mockHackerNewsService: any;

  beforeEach( () => {
     TestBed.configureTestingModule({
      declarations: [
        HackerNewsComponent,
      ],
      imports: [
        FormsModule,
      ],
      providers: [
        { provide: HackerNewsService, useValue: mockHackerNewsService },
      ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(HackerNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
