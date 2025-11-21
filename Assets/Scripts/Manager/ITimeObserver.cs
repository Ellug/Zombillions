
//GlobalTime 옵저버 인터페이스
public interface ITimeObserver
{
    // 낮/밤이 변할 때 동작하는 메서드 
    public void OnTimeZoneChange(Day newTimeZone);
}
