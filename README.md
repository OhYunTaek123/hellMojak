헬모작/HellMojak
-스팀 게임 헬테이커와 같이 탑다운 뷰 퍼즐 게임 방식 구현을 위하여 제작 시도

타일맵을 생성하고 메인 캐릭터인 manCha에 Script를 추가하여 조작이 가능하게 함

문제점 및 해결 방법
-씬변환 로딩바 및 오브젝트 중복 -> 싱글톤패턴을 사용함으로써 극
-키 동시입력 극복 -> ButtonUp 메소드를 통해 동시입력 제어
-상자 Lerp 이동시 정확하게 이동이 안됨 -> Lerp의 특징인 정수값으로 정확히 움직이지 않는 문제점을 특정 time이 지난후에는 원하는 값을 이통하게 조작
-포톤PUN2 로 서버 연동

![타일맵](https://github.com/OhYunTaek123/hellMojak/assets/128479666/483c4c6d-b627-4e75-9b16-cbe080e11545)
타일 맵 생성 및 collider 생성

![완료직전](https://github.com/OhYunTaek123/hellMojak/assets/128479666/0fd2c8de-1f68-4342-acc6-0a2961eabe00)
박스를 한칸씩 밀어 상자까지 도달

![씬변환로딩바](https://github.com/OhYunTaek123/hellMojak/assets/128479666/8d999bff-f21b-4dae-bf43-5f9673c46c71)
다음 단계를 통한 씬 변환시의 로딩바

![다음맵](https://github.com/OhYunTaek123/hellMojak/assets/128479666/13f4fe05-83bf-4ea3-b3ae-3460b917520a)
씬 변환 완료
