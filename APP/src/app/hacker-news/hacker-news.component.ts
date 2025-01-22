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
  itemIds: number[] = [];
  currentPage: number = 1;
  pageSize: number = 25;
  totalItems: number = 0;

  constructor(private hackerNewsService: HackerNewsService) { }

  ngOnInit(): void {
    this.fetchTopItems();
  }

  private fetchTopItems(): void {
    this.hackerNewsService.getTopItems().subscribe((ids) => {
      this.itemIds = ids;
      this.totalItems = ids.length;
      this.loadPage(this.currentPage, ids);
    });
  }

  private loadPage(page: number, itemIds: number[]): void {
    this.items = []; 
    const startIndex = (page - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;

    const itemSubset = itemIds.slice(startIndex, endIndex);
    itemSubset.forEach((id) => {
      this.hackerNewsService.getItem(id).subscribe((item) => {
        this.items.push(item);
      });
    });
  }

  changePage(page: number): void {
    if (page >= 1 && page <= Math.ceil(this.totalItems / this.pageSize)) {
      this.currentPage = page;
      this.loadPage(page, this.itemIds);
    }
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }
}
