import { Component, OnInit } from '@angular/core';

import { HackerNewsService } from './hacker-news.service';

@Component({
  selector: 'app-hacker-news',
  standalone: false,
  templateUrl: './hacker-news.component.html',
  styleUrl: './hacker-news.component.css'
})
export class HackerNewsComponent implements OnInit {
  items: any[] = [];

  constructor(private hackerNewsService: HackerNewsService) { }

  ngOnInit(): void {
    this.hackerNewsService.getTopStories().subscribe((itemIds) => {
      const topTenIds = itemIds.slice(0, 1000);
      topTenIds.forEach((id) => {
        this.hackerNewsService.getStory(id).subscribe((item) => {
          this.items.push(item);
        });
      });
    });
  }
}
