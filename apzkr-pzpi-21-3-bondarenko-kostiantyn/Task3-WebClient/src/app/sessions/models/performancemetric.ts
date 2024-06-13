export interface PerformanceMetric{
  sessionId: string;
  athleteId: string;
  metricType: string;
  metricValue: number;
  timeStamp: Date;
}
