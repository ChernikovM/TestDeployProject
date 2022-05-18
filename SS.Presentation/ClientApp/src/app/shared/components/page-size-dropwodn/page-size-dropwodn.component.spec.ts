import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSizeDropwodnComponent } from './page-size-dropwodn.component';

describe('PageSizeDropwodnComponent', () => {
  let component: PageSizeDropwodnComponent;
  let fixture: ComponentFixture<PageSizeDropwodnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageSizeDropwodnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageSizeDropwodnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
