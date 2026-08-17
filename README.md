# OverlappingArea
유니티로 두 원이 겹치는 부분의 넓이 구하기

## 만든 계기
교수님과 동기가 두 원이 겹쳐지는 구역의 넓이를 어떻게 구할지 의논하는 것을 보고 떠올라서 만들어보게 되었다.

## Idea
겹치지 않거나 작은 원이 큰 원 안에 포함되어있는 경우를 제외하고 아래 공식을 알면 구할 수 있었다.
  * [코사인 법칙](https://ko.wikipedia.org/wiki/%EC%BD%94%EC%82%AC%EC%9D%B8_%EB%B2%95%EC%B9%99)
  * [헤론의 공식](https://ko.wikipedia.org/wiki/%ED%97%A4%EB%A1%A0_%EA%B3%B5%EC%8B%9D)

겹친 부분의 넓이는 두 부채꼴의 넓이의 합 - 사각형(삼각형 * 2)이기 때문에 위 공식을 사용할 수 있다.

## Algorithm
두 원의 중심 사이 거리를 $d$, 각 원의 반지름을 $a$, $b$라고 하자

부분적으로 겹치는 부분의 넓이를 다음 함수로 정의한다.

```math
A(a,b,d)=
\begin{cases}
0& d \ge a+b\\
\pi r_\min^2& d+r_\min \le r_\max\\
f(a,b,d)& \text{otherwise}
\end{cases}
```

이 때 $r_{\min}, r_{\max}$는 다음과 같이 정의한다.

```math
r_\min = \min(a, b)
```
```math
r_\max = \max(a, b)
```

또한 $f(a, b, d)$는 다음과 같이 정의한다.

```math
f(a, b, d)=\frac{a^2\theta_1}2+\frac{b^2\theta_2}2-2\sqrt{s(s-a)(s-b)(s-d)}
```

```math
\theta_1=2\cos^{-1}\left(\frac{d^2+a^2-b^2}{2da}\right)
```

```math
\theta_2=2\cos^{-1}\left(\frac{d^2+b^2-a^2}{2db}\right)
```

```math
s=\frac{a+b+d}2
```

### 간단한 설명
1. 두 원이 겹치지 않으면($d\ge a+b$) $0$, 작은 원이 큰 원 안에 속해있다면($d+r_\min\le r_\max$) 작은 원의 넓이로 전처리한다.

2. 두 원의 중심과 두 교점 중 하나를 잇는 삼각형의 세 변의 길이 $a, b, d$에 [코사인 법칙](https://ko.wikipedia.org/wiki/%EC%BD%94%EC%82%AC%EC%9D%B8_%EB%B2%95%EC%B9%99)을 적용해 두 원의 중심과 교점을 잇는 선분 사이의 중심각을 구한다. 이 때 얻어진 각은 중심각의 절반이기 때문에 2배를 해준다.
    - 단 부채꼴의 넓이 공식은 $\frac{r^2\theta}2$이므로 코드에선 중심각의 절반을 구한 값에 반지름 제곱을 곱한다.

3. 두 원의 중심과 두 교점 중 하나를 잇는 삼각형의 넓이를 [헤론의 공식](https://ko.wikipedia.org/wiki/%ED%97%A4%EB%A1%A0_%EA%B3%B5%EC%8B%9D)으로 구하고 2배를 해 사각형의 넓이로 바꿔준다.

4. 중심각으로 부채꼴의 넓이를 구하고 사각형의 넓이를 빼면 겹친 부분의 넓이를 구할 수 있다.
