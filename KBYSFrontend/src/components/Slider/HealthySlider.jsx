import Slider from 'react-slick';
import './Slider.css';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import yemek1 from "../../assets/yemek1.png";
import yemek2 from "../../assets/yemek2.png";
import yemek3 from "../../assets/yemek3.png";
import yemek4 from "../../assets/yemek4.png";
import yemek5 from "../../assets/yemek5.png";


export function HealthySlider() {
    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 1,
        centerMode: true,
        centerPadding: '0',
        arrows: false,
    };

    const slides = [
        {
            id: 1,
            image: yemek1,
            title: 'Dengeli bir diyet, her gün çeşitli meyve ve sebzeleri içermelidir.',
        },
        {
            id: 2,
            image: yemek2,
            title: 'Her gün yeşil yapraklı sebzeler tüketmek sağlığınıza faydalıdır.',
        },
        {
            id: 3,
            image: yemek3,
            title: 'Kahvaltıda yulaf ezmesi tüketmek enerjinizi artırır ve uzun süre tok tutar.',
        },
        {
            id: 4,
            image: yemek4,
            title: 'Günlük su tüketiminizi artırarak vücudunuzun hidrat kalmasını sağlayın.',
        },
        {
            id: 5,
            image: yemek5,
            title: 'Günlük su tüketiminizi artırarak vücudunuzun hidrat kalmasını sağlayın.',
        },
    ];

    return (
        <div className="slider-container">
            <Slider {...settings}>
                {slides.map(slide => (
                    <div key={slide.id} className="slide">
                        <img src={slide.image} alt={slide.title} className="slide-image" />
                        <div className="slide-text-container">
                            <p className="slide-text">{slide.title}</p>
                        </div>
                    </div>
                ))}
            </Slider>
        </div>
    );
}
