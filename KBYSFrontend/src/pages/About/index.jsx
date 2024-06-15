import { useState } from 'react';
import './About.css'; // CSS dosyasını import edin
import logo from "../../assets/hakkımızda.png";
import profile1 from "../../assets/profile1.png";  
import profile2 from "../../assets/profile2.png"; 
import thx from "../../assets/thx.png"; 

const tabsData = {
  Tab1: [
    { id: 1, title: '', imgSrc: thx },
    { id: 2, title: '', imgSrc: profile1 },
    { id: 3, title: '', imgSrc: profile2 },
  ],
  Tab2: [
    { id: 2, title: '22380101019', imgSrc: profile2 }
  ],
  Tab3: [
    { id: 3, title: '22380101064', imgSrc: profile1 },

  ]
};

export function About() {
  const [activeTab, setActiveTab] = useState('Tab1');

  const handleTabClick = (tab) => {
    setActiveTab(tab);
  };

  return (
    <div>
      <div className="about-container">
        <div className="intro-text">
          <div className="portfolio-tag">MERHABA !</div>
          <h1>Kişiselleştirilmiş Beslenme ve Yemek Tarifi Sistemi ne Hoşgeldiniz !</h1>
          <p>
          Bu proje, Ecenur Sönmez ve Yusuf Yıldız tarafından Sistem Analizi ve Tasarımı dersi bitirme projesi olarak TÜBİTAK 2209-A kapsamında hazırlanmıştır. 
          Projemiz, kullanıcılardan alınan hastalık ve alerjen bilgilerine göre, onlara en uygun ve sağlıklı tarifleri tamamen kişiye özel olarak oluşturmaktadır.
           Kullanıcılar, dilerlerse besin değerlerine göre tarif arayabilir veya Tarifler sayfasından tamamen kendi yazdığımız yapay zekayı kullanarak
           ilk kez kendilerinin göreceği tarifler oluşturabilirler.
          </p>
          <button className="connect-button">Daha Fazlası <span>→</span></button>
        </div>
        <div className="logo-image">
          <img src={logo} alt="Astronaut" />
        </div>
      </div>

      <div className="skills-container">
        <h2>Proje İçeriği</h2>
        <p>
        Projemizin arka tarafı, Asp.Net Core Web Api kullanılarak yazılmıştır. 
        Tarifler kısmında ise kendi geliştirdiğimiz yapay zeka teknolojisi kullanılmaktadır. Ön tarafta ise React JS,
         Tailwind CSS ve jQuery kullanılarak dinamik ve kullanıcı dostu bir arayüz oluşturulmuştur.
        </p>
    <br />
        <div className="skills">
          <div className="skill">
            <div className="skill-circle">
              <div className="skill-percentage">react</div>
            </div>
            <p>Frontend</p>
          </div>
          <div className="skill">
            <div className="skill-circle">
              <div className="skill-percentage">c#</div>
            </div>
            <p>api/backend</p>
          </div>
          <div className="skill">
            <div className="skill-circle">
              <div className="skill-percentage">sql</div>
            </div>
            <p>postgre sql</p>
          </div>
        </div>
      </div>

      <div className="tabs">
        <button onClick={() => handleTabClick('Tab1')} className={activeTab === 'Tab1' ? 'active' : ''}>Teşekkürlerimiz</button>
        <button onClick={() => handleTabClick('Tab2')} className={activeTab === 'Tab2' ? 'active' : ''}>Yusuf Yıldız</button>
        <button onClick={() => handleTabClick('Tab3')} className={activeTab === 'Tab3' ? 'active' : ''}>Ecenur Sönmez</button>
      </div>

      <div className="tab-content">
        {tabsData[activeTab].map(item => (
          <div key={item.id} className="tab-item">
            <img src={item.imgSrc} alt={item.title} />
            <h3>{item.title}</h3>
          </div>
        ))}
      </div>
    </div>
  
  );
}
export default About;
