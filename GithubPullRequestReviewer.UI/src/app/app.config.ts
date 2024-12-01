import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';
import { ApiModule as PullRequestApiModule } from './api/pull-request/api.module';
import { ApiModule as EventHandlerApiModule } from './api/event-handler/api.module';
import { ApiModule as ReviewerApiModule } from './api/reviewer/api.module';
import { provideHighlightOptions } from 'ngx-highlightjs';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    provideHighlightOptions({
      fullLibraryLoader: () => import('highlight.js'),
      lineNumbersLoader: () => import('ngx-highlightjs/line-numbers'),
    }),
    importProvidersFrom(PullRequestApiModule.forRoot({ rootUrl: 'http://localhost:5124' })),
    importProvidersFrom(EventHandlerApiModule.forRoot({ rootUrl: 'http://localhost:5226' })),
    importProvidersFrom(ReviewerApiModule.forRoot({ rootUrl: 'http://localhost:5106' })),
  ]
};
