export interface HealthMetric{
  sessionId: string;
  athleteId: string;
  metricType: string;
  metricValue: number;
  timeStamp: Date;
}
