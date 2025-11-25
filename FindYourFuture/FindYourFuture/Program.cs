using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static string applicantsFile = "applicants.txt";
    static string desktopPath = @"C:\Users\CLienT\OneDrive\Desktop";

    static Dictionary<string, string> allJobs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
       // Medical
    { "Doctor", "Provides medical care to patients and diagnoses illnesses. Examines patients to determine health issues. Prescribes treatments, medications, or therapies. Monitors recovery and adjusts care as needed. Advises patients on preventive measures and lifestyle choices." },
    { "Nurse", "Cares for patients in hospitals, clinics, or homes. Administers medications and treatments as directed by doctors. Monitors patient conditions and reports changes. Provides emotional support and education to patients and families. Assists in medical procedures and daily care routines." },
    { "Dentist", "Diagnoses and treats dental problems in patients. Performs cleanings, fillings, extractions, and other procedures. Advises on oral hygiene and nutrition for healthy teeth. Monitors patient recovery after treatments. Educates patients on preventive dental care." },
    { "Pharmacist", "Prepares and dispenses medications safely and accurately. Advises patients and healthcare providers on proper drug use. Monitors for interactions and side effects. Counsels patients on health management and preventive care. Maintains records and ensures compliance with regulations." },
    { "Surgeon", "Performs surgical procedures to treat injuries, diseases, or deformities. Plans surgeries and evaluates patient readiness. Monitors patients during and after operations. Coordinates with medical teams for care and recovery. Advises patients on post-operative health and rehabilitation." },
    { "Medical Technologist", "Conducts laboratory tests to help diagnose health conditions. Analyzes blood, tissue, and other samples. Ensures accuracy and quality of test results. Reports findings to physicians and medical staff. Maintains lab equipment and follows safety protocols." },
    { "Radiologist", "Uses imaging techniques such as X-rays, CT scans, and MRIs to detect illnesses. Interprets results for accurate diagnosis. Advises other doctors on treatment options. Monitors patient progress through imaging studies. Keeps detailed reports for medical records and research." },
    { "Physical Therapist", "Designs rehabilitation programs to help patients regain mobility. Treats injuries or post-surgery conditions. Teaches exercises to improve strength and flexibility. Monitors patient progress and adjusts treatment plans. Educates patients on injury prevention and self-care." },
    { "Midwife", "Provides care to women during pregnancy, childbirth, and postpartum. Monitors mother and baby health throughout the process. Assists with labor and delivery in clinical or home settings. Educates mothers on newborn care and breastfeeding. Offers emotional support and health guidance for families." },
    { "Paramedic", "Responds to emergency medical situations promptly. Stabilizes patients and provides first aid on-site. Transports patients safely to hospitals. Communicates with medical staff to ensure continuity of care. Maintains emergency equipment and documentation." },
    { "Veterinarian", "Diagnoses and treats illnesses in animals. Performs surgeries, vaccinations, and routine check-ups. Advises owners on proper care and nutrition. Monitors recovery and prevents diseases. Educates clients on animal health and safety." },
    { "Public Health Officer", "Develops and implements community health programs. Monitors outbreaks and manages disease prevention efforts. Educates the public on healthy lifestyles and hygiene. Collaborates with healthcare providers and organizations. Evaluates program effectiveness and improves strategies." },
    { "Rehabilitation Specialist", "Assists patients recovering from injuries or disabilities. Designs individualized therapy and recovery plans. Monitors progress and adjusts programs. Supports patients with emotional and motivational guidance. Collaborates with medical staff and family members." },
    { "Speech Therapist", "Helps patients with speech, language, and communication difficulties. Assesses and diagnoses communication disorders. Develops personalized therapy plans. Works with families to reinforce practice at home. Tracks progress and adapts treatment methods." },
    { "Caregiver", "Assists elderly, disabled, or sick individuals with daily tasks. Provides personal care, including hygiene, meals, and medication reminders. Offers companionship and emotional support. Monitors health and reports changes to family or medical staff. Creates a safe and comfortable environment." },

    // Education
    { "Teacher", "Educates students in specific subjects or skills. Plans lessons and sets learning objectives. Evaluates student performance through assessments and feedback. Encourages critical thinking and creativity. Guides students in personal and academic growth." },
    { "Professor", "Teaches courses at the university level. Conducts research to advance knowledge in their field. Mentors and advises students on academic and career paths. Publishes research findings in journals or books. Participates in university administration and curriculum planning." },
    { "Tutor", "Provides individualized academic support to students. Explains concepts clearly and reinforces learning. Assists with homework and exam preparation. Monitors progress and adapts teaching methods. Offers encouragement and strategies to improve study skills." },
    { "Librarian", "Manages library collections and resources efficiently. Assists users in locating and using information. Organizes programs and educational activities for the community. Maintains digital and physical cataloging systems. Promotes literacy and lifelong learning." },
    { "School Principal", "Leads the administration and operations of a school. Oversees teachers, staff, and student activities. Ensures compliance with educational standards and policies. Implements programs to improve academic outcomes. Communicates with parents and the community about school matters." },
    { "Guidance Counselor", "Provides academic and personal guidance to students. Supports emotional wellbeing and addresses behavioral concerns. Helps students plan for careers or higher education. Mediates conflicts and provides crisis support. Coordinates with teachers and parents for student development." },
    { "Teaching Assistant", "Supports classroom teachers in lesson delivery. Prepares teaching materials and resources. Assists students who need extra help. Supervises classroom activities and maintains order. Provides feedback to teachers on student performance." },
    { "Curriculum Developer", "Designs and updates educational programs and materials. Ensures alignment with standards and learning objectives. Evaluates curriculum effectiveness through feedback and testing. Advises teachers on implementation methods. Integrates new technologies and teaching strategies." },
    { "Special Education Teacher", "Teaches students with learning or physical disabilities. Adapts lessons to meet individual needs. Collaborates with parents and specialists for support. Assesses student progress and adjusts teaching methods. Promotes inclusive education and personal development." },
    { "Researcher", "Conducts investigations to expand knowledge in education. Designs experiments or studies to answer questions. Analyzes data and draws conclusions. Publishes findings and recommends applications. Shares results with educators, policymakers, and the public." },
    { "Youth Worker", "Supports young people in personal, social, and educational development. Organizes programs and recreational activities. Provides guidance, mentoring, and advice. Helps address challenges such as behavior or family issues. Connects youth to community resources and opportunities." },
    { "Daycare Worker", "Cares for young children in daycare centers or homes. Plans and implements age-appropriate activities. Ensures children’s safety and health. Encourages learning through play and social interaction. Communicates with parents about child progress and needs." },
    { "Counselor", "Provides emotional and psychological support to individuals. Assesses personal or social problems and creates guidance plans. Conducts therapy or mentoring sessions. Helps clients develop coping and problem-solving skills. Maintains confidentiality and ethical standards." },
    { "Psychologist", "Studies human behavior and mental processes. Assesses and diagnoses psychological conditions. Provides therapy or interventions to improve mental health. Conducts research to understand behavior patterns. Advises individuals and organizations on psychological wellbeing." },
    { "Therapist", "Provides treatment for mental, emotional, or physical health issues. Designs individualized therapy programs. Monitors patient progress and adjusts approaches. Offers support for coping strategies and life skills. Works with other healthcare professionals as needed." },
    { "Community Worker", "Supports and develops local communities. Plans programs that address social or economic issues. Coordinates with residents and organizations for implementation. Provides education and resources to improve quality of life. Monitors outcomes and evaluates impact." },
    { "Social Worker", "Assists individuals, families, and groups in need. Provides counseling, advocacy, and practical support. Connects clients with resources and services. Addresses social issues like poverty, abuse, or unemployment. Promotes community wellbeing and social justice." },

    // Technology
    { "Software Developer", "Designs, codes, and tests software applications. Troubleshoots and debugs programs. Collaborates with teams on project requirements. Maintains and updates software for efficiency. Ensures software meets user needs and quality standards." },
    { "IT Support Specialist", "Assists users with computer or network problems. Installs and maintains hardware and software systems. Provides guidance and technical training. Troubleshoots issues and resolves them promptly. Documents solutions and maintains IT infrastructure." },
    { "Data Analyst", "Collects and analyzes data to identify trends and patterns. Prepares reports to support decision-making. Visualizes data for effective communication. Ensures accuracy and integrity of data. Advises teams or clients on insights and strategies." },
    { "Web Designer", "Creates website layouts and user interfaces. Designs graphics and content for visual appeal. Ensures usability and accessibility of websites. Collaborates with developers and content creators. Maintains and updates websites as needed." },
    { "Network Administrator", "Manages computer networks within organizations. Monitors network performance and security. Troubleshoots connectivity issues and implements solutions. Configures and maintains network devices and software. Documents system changes and network policies." },
    { "Cybersecurity Analyst", "Protects computer systems from cyber threats. Monitors for breaches and vulnerabilities. Develops security measures and protocols. Advises organizations on risk management. Investigates incidents and ensures data protection." },
    { "Database Administrator", "Maintains and organizes databases for businesses. Ensures data integrity, security, and accessibility. Monitors performance and optimizes queries. Backs up and restores data as needed. Supports users with database-related needs." },
    { "Cloud Engineer", "Designs and manages cloud computing systems. Monitors performance, security, and scalability. Implements cloud solutions for organizations. Troubleshoots and optimizes cloud resources. Collaborates with IT teams to ensure smooth operations." },
    { "Game Developer", "Designs and codes video games for entertainment or education. Creates gameplay mechanics, graphics, and sound. Tests and debugs game functionality. Works with designers and artists for cohesive results. Updates and maintains games post-launch." },
    { "AI Specialist", "Develops and implements artificial intelligence systems. Trains models using data and algorithms. Evaluates AI performance and adjusts approaches. Integrates AI solutions into applications. Monitors ethical and safety standards." },
    { "Software Engineer", "Designs, develops, and maintains software systems. Tests and debugs programs for efficiency. Collaborates with teams to meet project goals. Ensures software meets user and business needs. Keeps up with emerging technologies." },
    { "Computer Engineer", "Designs computer hardware and integrated systems. Develops software-hardware interaction solutions. Tests and optimizes performance. Troubleshoots issues and ensures reliability. Works on innovation and technical improvements." },

    // Business & Finance
{ "Accountant", "Prepares and examines financial records for businesses or individuals. Ensures accuracy and compliance with regulations. Analyzes financial statements to provide insights. Advises management on budgeting and cost control. Prepares reports for audits and tax purposes." },
{ "Manager", "Oversees teams, projects, and organizational operations. Plans, coordinates, and sets goals for departments. Monitors performance and provides feedback. Solves problems and makes strategic decisions. Communicates with staff and upper management effectively." },
{ "Banker", "Handles financial transactions and manages customer accounts. Advises clients on loans, investments, and savings. Evaluates creditworthiness for financing requests. Ensures compliance with banking regulations. Monitors market trends to inform clients and institutions." },
{ "HR Officer", "Manages recruitment, hiring, and employee relations. Develops workplace policies and ensures compliance. Conducts training and performance evaluations. Resolves conflicts and promotes employee engagement. Supports organizational goals through workforce planning." },
{ "Marketing Specialist", "Plans and executes marketing campaigns. Analyzes market trends and consumer behavior. Develops promotional materials and advertising strategies. Collaborates with sales and product teams. Monitors campaign performance and adjusts strategies accordingly." },
{ "Business Analyst", "Studies business processes to improve efficiency. Identifies areas for improvement and recommends solutions. Gathers and analyzes data for decision-making. Communicates findings to management and stakeholders. Supports project implementation and monitoring." },
{ "Sales Executive", "Promotes and sells products or services to clients. Builds relationships and maintains customer satisfaction. Negotiates contracts and pricing. Tracks sales performance and meets targets. Provides market feedback to inform business strategy." },
{ "Entrepreneur", "Starts and manages new business ventures. Identifies market opportunities and develops business plans. Secures funding and manages finances. Oversees operations and hires staff. Adapts to challenges and drives growth through innovation." },
{ "Project Manager", "Plans and coordinates projects from initiation to completion. Allocates resources and manages timelines. Monitors progress and ensures quality standards. Communicates with stakeholders and resolves issues. Evaluates project outcomes and documents lessons learned." },
{ "Auditor", "Examines financial records to ensure accuracy and compliance. Identifies discrepancies or inefficiencies. Prepares reports and recommendations for improvement. Advises management on financial risk and control measures. Maintains ethical and professional standards." },
{ "Financial Analyst", "Analyzes financial data to guide investment decisions. Prepares reports and forecasts for businesses or clients. Evaluates market trends and risks. Recommends strategies to improve financial performance. Monitors portfolio performance and adjusts plans accordingly." },
{ "Stockbroker", "Buys and sells stocks on behalf of clients. Monitors market movements and advises clients. Develops investment strategies to meet financial goals. Maintains strong client relationships. Ensures compliance with financial regulations." },
{ "Insurance Agent", "Sells insurance policies and advises clients on coverage options. Evaluates risks and determines policy terms. Helps clients file claims and resolves issues. Builds and maintains customer relationships. Keeps up-to-date with industry regulations and products." },
{ "Loan Officer", "Evaluates and approves loan applications. Advises clients on financing options. Assesses creditworthiness and risk. Manages loan documentation and compliance. Monitors repayments and handles problem accounts." },
{ "Investment Consultant", "Provides advice on investment opportunities and strategies. Analyzes financial markets and trends. Develops personalized investment plans. Monitors client portfolios and performance. Ensures compliance with financial regulations." },
{ "Tax Advisor", "Prepares and reviews tax returns for individuals or businesses. Advises on tax planning and compliance. Identifies tax-saving opportunities. Represents clients in tax audits or disputes. Keeps updated on tax laws and regulations." },
{ "Payroll Specialist", "Manages employee salary, benefits, and deductions. Ensures accurate and timely payroll processing. Maintains payroll records and documentation. Advises management on compensation policies. Ensures compliance with labor laws and regulations." },
{ "Economist", "Studies economic trends and analyzes data. Advises businesses or governments on policy and strategy. Prepares reports and forecasts for decision-making. Researches issues like inflation, unemployment, and growth. Evaluates the impact of economic changes on society." },

// Trades & Manual Skills
{ "Carpenter", "Constructs and repairs wooden structures and furniture. Reads blueprints and measures materials. Uses tools and machines safely. Finishes surfaces and ensures structural integrity. Maintains a clean and safe work environment." },
{ "Electrician", "Installs and repairs electrical systems and wiring. Troubleshoots faults and ensures safety standards. Reads blueprints and technical diagrams. Maintains electrical equipment and performs inspections. Advises clients on proper usage and maintenance." },
{ "Plumber", "Installs and repairs water supply and drainage systems. Diagnoses pipe and fixture problems. Ensures compliance with plumbing codes. Maintains tools and safety equipment. Advises clients on maintenance and water efficiency." },
{ "Mechanic", "Repairs and maintains vehicles or machinery. Diagnoses mechanical issues and performs fixes. Tests performance to ensure reliability. Keeps detailed maintenance records. Advises on preventive maintenance and upgrades." },
{ "Welder", "Joins metal components using welding techniques. Reads technical drawings and plans. Ensures precision and safety during operations. Inspects finished products for quality. Maintains welding equipment and materials." },
{ "Mason", "Builds structures with bricks, stones, or concrete. Follows architectural plans accurately. Mixes materials to achieve desired consistency. Ensures safety and stability of constructions. Finishes surfaces and performs repairs as needed." },
{ "Painter", "Prepares and paints surfaces in buildings or structures. Chooses appropriate paints and tools. Applies coatings evenly and precisely. Maintains safety and work cleanliness. Advises clients on colors and finishes." },
{ "Tailor", "Designs, alters, and repairs clothing. Measures clients and creates patterns. Chooses fabrics and sewing techniques. Ensures fit, quality, and aesthetics. Maintains sewing equipment and inventory." },
{ "Blacksmith", "Shapes and forges metal using heat and tools. Creates functional or artistic objects. Repairs metal equipment or structures. Ensures quality and durability of products. Maintains safety standards in the workshop." },
{ "Butcher", "Cuts and prepares meat for sale or consumption. Ensures hygiene and safety standards. Trims, packages, and stores meat properly. Advises customers on cuts and cooking methods. Maintains tools and work area cleanliness." },

// Arts & Media
{ "Graphic Designer", "Creates visual content for digital and print media. Designs layouts, illustrations, and branding materials. Collaborates with clients and teams. Ensures designs meet objectives and standards. Keeps updated on design trends and software." },
{ "Musician", "Performs music for entertainment or education. Composes or arranges pieces. Practices regularly to maintain skill. Records or performs live for audiences. Collaborates with other musicians and producers." },
{ "Writer", "Produces written content for books, articles, or scripts. Researches topics thoroughly. Edits and revises work for clarity and quality. Publishes content for different audiences. Collaborates with editors and publishers." },
{ "Photographer", "Captures images for personal, artistic, or commercial purposes. Plans compositions and selects equipment. Edits and retouches photos for quality. Works with clients or teams on projects. Maintains and manages photography equipment." },
{ "Actor", "Performs roles in films, theater, or television. Studies scripts and develops characters. Practices and rehearses regularly. Works with directors, cast, and crew. Engages audiences through authentic performance." },
{ "Film Director", "Oversees the production of movies or videos. Guides the artistic and technical aspects. Collaborates with actors and crew. Plans shooting schedules and manages resources. Reviews and finalizes the project to meet vision." },
{ "Dancer", "Performs choreographed routines in shows or competitions. Practices and maintains physical fitness. Expresses stories or emotions through movement. Collaborates with choreographers and performers. Performs live or records for audiences." },
{ "Fashion Designer", "Designs clothing, footwear, and accessories. Chooses fabrics, patterns, and colors. Sketches designs and supervises production. Presents collections to clients or shows. Stays updated on fashion trends and industry standards." },
{ "Animator", "Produces animated content for media, games, or film. Creates storyboards and character designs. Develops motion sequences using software. Collaborates with teams for cohesive storytelling. Reviews and refines animations for quality." },
{ "Illustrator", "Creates drawings for books, advertisements, or media. Uses traditional or digital tools. Develops concepts according to client needs. Edits and finalizes artwork for publication. Collaborates with writers or designers." },
{ "Painter (art)", "Creates visual art using paints on various surfaces. Plans and sketches compositions. Chooses colors and techniques to convey messages. Exhibits or sells artworks to audiences. Continues skill development through practice and study." },
{ "Makeup Artist", "Applies makeup for events, fashion, or media. Analyzes clients’ skin and preferences. Selects products and techniques to achieve desired looks. Maintains hygiene and safety standards. Advises clients on skin care and style." },
{ "Jewelry Designer", "Designs and creates wearable jewelry. Chooses materials and crafting techniques. Sketches and models designs. Produces finished pieces for clients or sale. Keeps updated on trends and customer preferences." },
{ "Interior Designer", "Plans and decorates interior spaces. Chooses furniture, colors, and materials. Ensures functionality and aesthetics. Coordinates with clients and contractors. Monitors project execution and adjustments." },
{ "Sculptor", "Creates three-dimensional art from materials like clay, stone, or metal. Develops concepts and sketches. Shapes and refines pieces with tools. Exhibits or sells finished artwork. Experiments with techniques and materials for growth." },
{ "Architect", "Designs buildings and structures with functionality and safety in mind. Prepares blueprints and plans. Coordinates with engineers and clients. Oversees construction progress and compliance. Updates designs based on project requirements." },
{ "Industrial Designer", "Creates product designs balancing function and style. Researches user needs and market trends. Sketches and models prototypes. Collaborates with engineers and manufacturers. Tests and refines products for usability and aesthetics." },
{ "Copywriter", "Writes content for advertising and marketing purposes. Develops compelling messages for target audiences. Collaborates with marketing teams. Edits and refines copy to improve effectiveness. Adapts tone and style to client needs." },
{ "Content Creator", "Produces digital media content like videos, blogs, or social posts. Plans and researches topics. Edits and optimizes content for platforms. Engages with audiences to build following. Analyzes performance and adjusts strategies." },
{ "Social Media Manager", "Manages social media accounts for organizations. Creates content and posts regularly. Monitors engagement and responds to followers. Analyzes analytics to improve strategies. Plans campaigns to grow audience and reach." },
{ "Editor", "Reviews and revises written content for clarity and correctness. Checks grammar, style, and consistency. Provides feedback to writers. Ensures content meets publication standards. Collaborates with authors and publishers." },
{ "Journalist", "Researches and reports news stories for media outlets. Conducts interviews and gathers information. Writes articles or scripts with accuracy. Verifies facts and sources. Delivers content in print, broadcast, or digital formats." },
{ "News Anchor", "Presents news stories on television or radio. Reads scripts clearly and accurately. Engages with audiences professionally. Interviews guests and reports live events. Coordinates with reporters and producers." },
{ "Radio DJ", "Hosts radio shows and selects music playlists. Engages listeners with commentary and announcements. Interviews guests and promotes events. Operates broadcasting equipment. Plans show content to entertain and inform." },
{ "TV Producer", "Plans and oversees television program production. Coordinates teams of writers, directors, and crew. Manages budgets and schedules. Ensures quality and compliance with broadcasting standards. Evaluates program success and implements improvements." },

// Public Service
{ "Police Officer", "Enforces laws and maintains public safety. Responds to emergencies and criminal activity. Investigates incidents and gathers evidence. Protects citizens and property. Works with the community to prevent crime." },
{ "Firefighter", "Responds to fires and emergency situations. Rescues individuals in danger. Operates firefighting equipment safely. Conducts fire prevention education. Collaborates with other emergency services." },
{ "Soldier", "Serves in the military to protect the nation. Follows training and operational orders. Engages in defense and security operations. Maintains discipline and physical fitness. Participates in humanitarian and peacekeeping missions." },
{ "Government Worker", "Provides services in government departments or agencies. Implements policies and programs. Assists citizens with inquiries or applications. Maintains accurate records and reports. Supports public administration effectively." },
{ "Customs Officer", "Monitors goods and people crossing borders. Ensures compliance with import/export regulations. Inspects cargo and documentation. Detects and prevents illegal activities. Provides guidance on customs procedures." },
{ "Postal Worker", "Delivers letters and packages to recipients. Sorts and organizes mail efficiently. Ensures timely and accurate delivery. Handles customer inquiries and complaints. Maintains records and safety standards." },
{ "Politician", "Represents constituents in government. Develops and proposes policies and laws. Engages with the community to address concerns. Participates in debates and decision-making. Works to achieve political and social goals." },
{ "Traffic Enforcer", "Directs vehicle and pedestrian traffic. Ensures road safety and law compliance. Responds to accidents and incidents. Provides guidance to drivers and pedestrians. Maintains order on busy streets and intersections." },
{ "Barangay Captain", "Leads the administration of a local community. Implements programs and policies for residents. Resolves community conflicts and concerns. Coordinates with government and organizations. Oversees development and safety initiatives." },
{ "Security Guard", "Protects people, property, and assets. Monitors premises and prevents unauthorized access. Responds to emergencies and incidents. Reports suspicious activity to authorities. Maintains security equipment and protocols." },
{ "Bodyguard", "Provides personal protection to clients. Assesses threats and plans security measures. Escorts clients to locations safely. Responds quickly to dangerous situations. Maintains discretion and professionalism." },
{ "Safety Inspector", "Ensures workplaces follow safety regulations. Conducts inspections and identifies hazards. Recommends safety improvements. Documents compliance and incidents. Educates employees on safety practices." },
{ "Forensic Investigator", "Examines crime scenes to collect evidence. Analyzes physical, biological, or chemical traces. Prepares reports for legal proceedings. Collaborates with law enforcement. Provides expert testimony when required." },
{ "Crime Scene Analyst", "Studies evidence from crime scenes for investigation. Uses forensic techniques to analyze samples. Documents findings accurately. Assists in identifying suspects or causes. Supports law enforcement with expert advice." },
{ "Detective", "Investigates crimes to solve cases. Gathers and analyzes evidence. Interviews witnesses and suspects. Prepares detailed reports and case files. Works with law enforcement to apprehend offenders." },
{ "Customs Security Officer", "Monitors and secures borders. Inspects goods and people for legal compliance. Detects illegal activity and prevents smuggling. Collaborates with law enforcement agencies. Maintains records and security protocols." },
{ "Prison Guard", "Oversees and supervises inmates in correctional facilities. Ensures safety and security of staff and prisoners. Monitors daily routines and behavior. Enforces rules and handles emergencies. Reports incidents and maintains records." },
{ "Cybersecurity Specialist", "Protects computer systems from unauthorized access. Monitors for cyber threats and vulnerabilities. Implements security measures and protocols. Responds to breaches and incidents. Advises organizations on safe practices." },
{ "Lifeguard", "Monitors swimming areas to ensure safety. Responds to emergencies and rescues individuals. Enforces pool or beach rules. Maintains first aid and rescue equipment. Educates the public on water safety." },

// Engineering & Science
{ "Civil Engineer", "Designs and supervises construction of buildings and infrastructure. Prepares plans and technical drawings. Monitors construction progress and ensures safety. Coordinates with contractors and stakeholders. Evaluates projects for quality and compliance." },
{ "Mechanical Engineer", "Develops and maintains mechanical systems and machines. Designs, tests, and improves equipment. Troubleshoots problems and optimizes performance. Ensures compliance with safety and industry standards. Collaborates with teams on engineering projects." },
{ "Electrical Engineer", "Works with electrical systems and power generation. Designs circuits and equipment. Tests and maintains electrical devices. Ensures safety and compliance with regulations. Collaborates with technical teams on projects." },
{ "Chemical Engineer", "Develops processes for chemicals, fuels, and materials. Designs and optimizes production systems. Ensures safety and environmental compliance. Conducts experiments and analyzes results. Improves processes for efficiency and sustainability." },
{ "Aerospace Engineer", "Designs aircraft, spacecraft, and related systems. Conducts simulations and tests for performance. Collaborates with engineering teams on projects. Ensures compliance with safety and industry standards. Improves designs for efficiency and innovation." },
{ "Biomedical Engineer", "Develops medical devices and technologies. Tests and evaluates equipment for safety and effectiveness. Collaborates with healthcare professionals. Designs solutions to improve patient care. Maintains compliance with regulations." },
{ "Agricultural Engineer", "Designs tools and systems to improve farming productivity. Develops irrigation, machinery, and storage solutions. Ensures sustainability and efficiency. Collaborates with farmers and organizations. Monitors and evaluates project results." },
{ "Environmental Engineer", "Develops solutions to protect the environment. Designs systems for pollution control and waste management. Conducts environmental assessments. Advises organizations on sustainability. Monitors compliance with environmental regulations." },
{ "Environmental Scientist", "Studies natural systems and human impact. Collects and analyzes environmental data. Develops solutions for conservation and protection. Advises governments and organizations. Publishes findings and reports on sustainability issues." },
{ "Biologist", "Studies living organisms and ecosystems. Conducts research and experiments. Observes behavior and environmental interactions. Publishes findings for scientific understanding. Advises on conservation and biodiversity." },
{ "Chemist", "Analyzes and experiments with chemicals. Develops new compounds and processes. Ensures safety in handling substances. Conducts research for industrial or medical applications. Records and reports findings accurately." },
{ "Physicist", "Studies natural laws of matter and energy. Conducts experiments and simulations. Analyzes data and tests hypotheses. Publishes research and presents findings. Advises on technological or scientific applications." },
{ "Research Scientist", "Conducts scientific investigations to generate new knowledge. Designs experiments and collects data. Analyzes results and draws conclusions. Publishes findings in scientific journals. Collaborates with peers for interdisciplinary studies." },
{ "Astronomer", "Studies stars, planets, and celestial phenomena. Observes and collects astronomical data. Analyzes findings using scientific methods. Publishes research and theories. Advises on space exploration and education." },
{ "Archaeologist", "Investigates human history through artifacts and sites. Conducts excavations and surveys. Analyzes findings and documents discoveries. Publishes reports and shares knowledge. Preserves and protects cultural heritage." },
{ "Anthropologist", "Studies human societies, cultures, and evolution. Conducts fieldwork and interviews. Analyzes social patterns and practices. Publishes findings and advises on cultural understanding. Explores relationships between history and modern society." },
{ "Geologist", "Examines rocks, soil, and Earth processes. Conducts field studies and collects samples. Analyzes geological data to understand natural phenomena. Advises on natural resources and hazards. Publishes research and reports for scientific use." },
{ "Marine Biologist", "Studies ocean life and ecosystems. Conducts field research and observations. Analyzes aquatic species and habitats. Advises on conservation and environmental protection. Publishes findings and educates the public." },
{ "Lab Technician", "Prepares and conducts laboratory experiments. Maintains equipment and ensures safety. Collects and records data accurately. Assists scientists and researchers in studies. Analyzes results and reports findings." },


    // Hospitality & Travel
{ "Hotel Manager", "Oversees hotel operations and staff management. Ensures guest satisfaction and service quality. Plans budgets and coordinates resources. Develops marketing and promotional strategies. Monitors performance and implements improvements." },
{ "Chef", "Prepares and creates meals in kitchens. Develops menus and plans recipes. Manages kitchen staff and operations. Ensures food quality, safety, and hygiene. Innovates dishes to meet customer preferences." },
{ "Tour Guide", "Leads visitors to historical or cultural sites. Explains significance and stories of locations. Ensures safety and engagement of participants. Coordinates schedules and logistics. Answers questions and provides recommendations." },
{ "Flight Attendant", "Assists passengers during flights. Ensures safety and compliance with regulations. Provides food and services onboard. Responds to emergencies and passenger needs. Maintains professional and friendly conduct." },
{ "Travel Agent", "Plans and books trips for individuals or groups. Advises clients on destinations and itineraries. Arranges transportation and accommodations. Handles travel documents and logistics. Provides solutions for travel issues and changes." },
{ "Cruise Ship Staff", "Performs various roles to serve passengers on cruises. Assists with activities, services, and hospitality. Ensures safety and comfort of guests. Coordinates with other crew members. Maintains professional conduct and cleanliness." },
{ "Bartender", "Prepares and serves drinks to customers. Creates cocktails and follows recipes. Maintains bar stock and cleanliness. Engages with patrons professionally. Ensures responsible service and safety." },
{ "Housekeeper", "Cleans and maintains hotel or household spaces. Arranges rooms and facilities for guests or residents. Uses cleaning supplies and equipment safely. Reports maintenance issues. Ensures hygiene and comfort standards." },
{ "Concierge", "Assists guests with requests, bookings, and information. Provides recommendations on local attractions and services. Handles reservations and special arrangements. Communicates effectively with clients and staff. Ensures a positive guest experience." },
{ "Resort Manager", "Manages resort operations and staff. Oversees guest services and satisfaction. Develops budgets and operational plans. Implements marketing strategies. Monitors performance and resolves issues." },

// Agriculture & Fishing
{ "Farmer", "Grows crops and raises animals for food or materials. Plans planting, harvesting, and maintenance. Monitors soil, water, and weather conditions. Manages equipment and resources. Adapts practices for productivity and sustainability." },
{ "Fisherman", "Catches fish and seafood for consumption or trade. Prepares equipment and boats for fishing. Monitors water and weather conditions. Processes and stores catches properly. Sells or distributes fish to markets or clients." },
{ "Horticulturist", "Cultivates plants, flowers, and gardens. Studies plant growth and soil conditions. Plans landscaping and plant arrangements. Advises on care, pests, and nutrition. Conducts research to improve plant health and yield." },
{ "Park Ranger", "Protects parks, wildlife, and natural resources. Monitors visitors and ensures safety regulations. Educates the public on conservation. Conducts patrols and maintenance tasks. Responds to emergencies and environmental concerns." },
{ "Agribusiness Manager", "Oversees agricultural business operations. Plans budgets, sales, and production. Manages staff and resources efficiently. Analyzes market trends and opportunities. Ensures sustainability and profitability." },
{ "Soil Scientist", "Studies soil properties and composition. Advises on agriculture, construction, or conservation. Conducts field tests and laboratory analysis. Interprets results and makes recommendations. Publishes findings and contributes to research." },
{ "Forestry Worker", "Manages and protects forests and trees. Conducts planting, maintenance, and conservation work. Monitors wildlife and ecosystem health. Uses tools and equipment safely. Educates the public about forest preservation." },

// Transport & Logistics
{ "Pilot", "Operates aircraft to transport passengers or cargo. Plans flight paths and monitors weather conditions. Ensures safety and compliance with aviation regulations. Coordinates with air traffic control and crew. Responds to emergencies effectively." },
{ "Driver", "Operates vehicles for transporting goods or people. Maintains vehicle condition and safety. Follows traffic regulations and routes efficiently. Handles deliveries or pickups responsibly. Communicates with clients and supervisors." },
{ "Ship Captain", "Commands ships and ensures safe navigation. Manages crew and ship operations. Monitors weather and sea conditions. Ensures cargo and passenger safety. Maintains compliance with maritime regulations." },
{ "Train Operator", "Drives trains according to schedules and safety protocols. Monitors speed, signals, and track conditions. Communicates with control centers and staff. Responds to emergencies or mechanical issues. Ensures passenger and cargo safety." },
{ "Truck Driver", "Transports goods over short or long distances. Maintains vehicle and ensures safe operation. Plans routes and manages schedules. Handles loading and unloading of cargo. Follows regulations and delivers on time." },
{ "Bus Conductor", "Assists passengers with tickets and information. Manages boarding and seating. Collects fares and ensures passenger safety. Provides customer service and guidance. Coordinates with drivers and transportation staff." },
{ "Air Traffic Controller", "Monitors aircraft movements in air and on runways. Provides pilots with instructions for safe operations. Coordinates takeoffs, landings, and routes. Responds to emergencies efficiently. Ensures compliance with aviation safety standards." },
{ "Seafarer", "Works aboard ships in various operational roles. Maintains equipment and monitors safety. Assists with navigation and cargo handling. Follows procedures and responds to emergencies. Collaborates with crew for smooth operations." },
{ "Delivery Rider", "Transports packages and goods locally. Plans routes for efficiency. Ensures safe and timely deliveries. Handles customer interactions professionally. Maintains vehicle and follows traffic regulations." },
{ "Logistics Coordinator", "Plans and organizes transport and delivery operations. Coordinates schedules, inventory, and staff. Monitors efficiency and resolves issues. Communicates with clients and suppliers. Optimizes logistics processes." },
{ "Warehouse Worker", "Manages storage, movement, and organization of goods. Receives, stores, and dispatches items. Operates equipment safely. Maintains inventory records. Ensures cleanliness and security of the warehouse." },
{ "Logistics Manager", "Oversees supply chain and transportation operations. Develops strategies for efficiency. Manages staff and resources. Coordinates with clients and partners. Monitors performance and resolves issues." },
{ "Delivery Driver", "Transports goods or packages safely and on time. Maintains vehicle and ensures road safety. Plans routes and schedules. Handles customer communication. Reports incidents or delivery issues." },
{ "Supply Chain Analyst", "Studies and improves supply operations. Collects and analyzes data. Identifies bottlenecks and recommends solutions. Collaborates with teams to implement improvements. Monitors performance and reports results." },
{ "Inventory Clerk", "Tracks and records stock items accurately. Updates databases and documentation. Assists with ordering and restocking. Conducts audits to ensure accuracy. Communicates inventory status to management." },
{ "Forklift Operator", "Operates machinery to move heavy goods. Loads and unloads materials safely. Maintains equipment in working condition. Follows safety regulations. Assists in warehouse or logistics operations." },
{ "Procurement Specialist", "Manages purchasing of supplies for organizations. Evaluates suppliers and negotiates contracts. Ensures timely delivery and quality of goods. Maintains records of procurement activities. Collaborates with teams to meet operational needs." },
{ "Shipping Coordinator", "Organizes and monitors product shipments. Plans schedules and routes. Communicates with carriers and clients. Tracks packages to ensure timely delivery. Resolves shipping issues and reports performance." },
{ "Freight Forwarder", "Arranges international cargo transportation. Handles customs documentation and regulations. Coordinates with carriers and clients. Monitors shipment progress. Ensures timely and safe delivery of goods." },
{ "Distribution Manager", "Oversees distribution of products to retailers or clients. Manages staff and logistics operations. Ensures timely delivery and inventory management. Develops strategies to optimize efficiency. Monitors performance and implements improvements." },

// Sports & Fitness
{ "Athlete", "Trains and competes in sports professionally. Maintains physical fitness and nutrition. Develops skills and strategies for performance. Participates in competitions or events. Works with coaches and teams for improvement." },
{ "Coach", "Guides and trains athletes or sports teams. Develops practice routines and strategies. Monitors performance and provides feedback. Ensures safety and motivation. Plans participation in competitions and events." },
{ "Fitness Trainer", "Designs exercise programs for individuals or groups. Monitors client progress and health. Demonstrates proper techniques and safety. Motivates clients to achieve goals. Adapts programs to meet specific needs." },
{ "Sports Official", "Enforces rules during sports events. Monitors player conduct and safety. Makes decisions on plays or disputes. Communicates with teams and organizers. Ensures fair and smooth conduct of competitions." },
{ "Gym Instructor", "Leads workouts and fitness classes. Demonstrates proper techniques and exercises. Monitors participant safety and progress. Provides guidance on fitness and nutrition. Motivates clients to achieve goals." },
{ "Referee", "Judges and enforces rules in sports games. Monitors player conduct and performance. Makes decisions on disputes and penalties. Communicates clearly with teams and officials. Ensures fair play and safety." },
{ "Physical Therapist (sports)", "Helps athletes recover from injuries. Designs rehabilitation programs. Monitors progress and adjusts treatment. Advises on injury prevention. Collaborates with coaches and medical staff." },
{ "Sports Commentator", "Provides live analysis of sports events. Explains plays and strategies to audiences. Engages viewers with insights and storytelling. Prepares research on teams and athletes. Works on TV, radio, or digital platforms." },
{ "Athletic Scout", "Identifies and evaluates talented athletes. Attends games and competitions to observe performance. Reports findings to teams or organizations. Recommends recruitment or training strategies. Maintains knowledge of sports trends and potential." },
{ "Nutritionist", "Advises on healthy diets and nutrition. Plans meal programs for individuals or groups. Monitors client progress and health. Educates on proper eating habits. Collaborates with healthcare or fitness professionals." },

// Law & Legal
{ "Lawyer", "Provides legal advice and representation to clients. Researches laws and case precedents. Drafts legal documents and contracts. Represents clients in court proceedings. Ensures clients understand their rights and options." },
{ "Judge", "Presides over court cases and hearings. Interprets and applies laws. Makes rulings and sentences based on evidence. Ensures fair and impartial trials. Maintains courtroom order and decorum." },
{ "Paralegal", "Assists lawyers with research and documentation. Prepares legal papers and case files. Conducts fact-checking and interviews. Supports clients with procedural matters. Maintains accurate and organized records." },
{ "Legal Assistant", "Provides administrative support in legal offices. Manages schedules and correspondence. Prepares documents and files. Communicates with clients and court officials. Supports lawyers in case preparation and operations." },
{ "Prosecutor", "Represents the state in criminal cases. Reviews evidence and prepares charges. Presents cases in court and examines witnesses. Advises law enforcement on legal matters. Ensures justice is served according to law." },
{ "Defense Attorney", "Defends individuals accused of crimes. Reviews evidence and prepares legal strategies. Represents clients in court proceedings. Advises clients on legal rights and options. Negotiates settlements and plea agreements if needed." },
{ "Court Clerk", "Manages court records and proceedings. Prepares legal documents and dockets. Coordinates communication between judges, lawyers, and parties. Maintains accurate files and archives. Supports the smooth operation of the court." },
{ "Notary Public", "Certifies legal documents and signatures. Verifies identities of signers. Ensures compliance with laws and regulations. Maintains records of notarizations. Provides impartial witnessing for legal purposes." },
{ "Legal Researcher", "Studies laws, cases, and legal trends. Collects and analyzes legal information. Prepares reports and summaries for lawyers or organizations. Supports case preparation and policy development. Keeps up-to-date with legal changes." },
{ "Compliance Officer", "Ensures organizations follow laws and regulations. Monitors operations for compliance risks. Develops policies and procedures to maintain standards. Trains staff on regulatory requirements. Reports and advises management on compliance issues." }
};  

    static Dictionary<string, string[]> jobTags = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
    {
        { "Doctor", new[]{ "healthcare", "diagnosis", "treatment", "patient care" } },
    { "Nurse", new[]{ "patient care", "hospital", "support", "treatment" } },
    { "Dentist", new[]{ "oral health", "prevention", "treatment" } },
    { "Pharmacist", new[]{ "medicine", "safety", "prescriptions" } },
    { "Surgeon", new[]{ "surgery", "precision", "emergency" } },
    { "Medical Technologist", new[]{ "lab", "analysis", "diagnosis" } },
    { "Radiologist", new[]{ "imaging", "diagnosis", "hospital" } },
    { "Physical Therapist", new[]{ "rehabilitation", "mobility", "patient care" } },
    { "Midwife", new[]{ "childbirth", "support", "patient care" } },
    { "Paramedic", new[]{ "emergency", "first aid", "patient care" } },

    { "Teacher", new[]{ "lesson", "curriculum", "guidance" } },
    { "Professor", new[]{ "lectures", "research", "mentoring" } },
    { "Tutor", new[]{ "lesson", "support", "guidance" } },
    { "Librarian", new[]{ "organization", "resources", "information" } },
    { "School Principal", new[]{ "leadership", "management", "planning" } },
    { "Guidance Counselor", new[]{ "advising", "student support", "career" } },
    { "Teaching Assistant", new[]{ "support", "lesson", "assisting" } },
    { "Curriculum Developer", new[]{ "planning", "lesson", "education" } },
    { "Special Education Teacher", new[]{ "special needs", "teaching", "support" } },
    { "Researcher", new[]{ "research", "analysis", "education" } },

    { "Software Developer", new[]{ "programming", "software", "development" } },
    { "IT Support Specialist", new[]{ "support", "technical", "network" } },
    { "Data Analyst", new[]{ "data", "analysis", "reporting" } },
    { "Web Designer", new[]{ "web", "design", "UI" } },
    { "Network Administrator", new[]{ "network", "maintenance", "management" } },
    { "Cybersecurity Analyst", new[]{ "security", "network", "protection" } },
    { "Database Administrator", new[]{ "database", "management", "organization" } },
    { "Cloud Engineer", new[]{ "cloud", "computing", "systems" } },
    { "Game Developer", new[]{ "game", "design", "coding" } },
    { "AI Specialist", new[]{ "AI", "machine learning", "systems" } },

    { "Accountant", new[]{ "finance", "auditing", "records" } },
    { "Manager", new[]{ "leadership", "management", "planning" } },
    { "Banker", new[]{ "finance", "accounts", "money" } },
    { "HR Officer", new[]{ "human resources", "management", "support" } },
    { "Marketing Specialist", new[]{ "marketing", "promotion", "strategy" } },
    { "Business Analyst", new[]{ "analysis", "process", "strategy" } },
    { "Sales Executive", new[]{ "sales", "customers", "promotion" } },
    { "Entrepreneur", new[]{ "business", "management", "creativity" } },
    { "Project Manager", new[]{ "planning", "management", "projects" } },
    { "Auditor", new[]{ "finance", "audit", "compliance" } },
        { "Financial Analyst", new[]{ "finance", "analysis", "investments" } },
    { "Stockbroker", new[]{ "finance", "stocks", "trading" } },
    { "Insurance Agent", new[]{ "finance", "insurance", "sales" } },
    { "Loan Officer", new[]{ "finance", "loans", "approval" } },
    { "Investment Consultant", new[]{ "finance", "advice", "investments" } },
    { "Tax Advisor", new[]{ "finance", "tax", "compliance" } },
    { "Payroll Specialist", new[]{ "finance", "payroll", "records" } },
    { "Economist", new[]{ "finance", "analysis", "markets" } },

    { "Carpenter", new[]{ "construction", "woodwork", "repair" } },
    { "Electrician", new[]{ "wiring", "installation", "repair" } },
    { "Plumber", new[]{ "pipes", "installation", "maintenance" } },
    { "Mechanic", new[]{ "vehicles", "repair", "maintenance" } },
    { "Welder", new[]{ "metalwork", "welding", "construction" } },
    { "Mason", new[]{ "construction", "bricks", "stones" } },
    { "Painter", new[]{ "painting", "art", "protection" } },
    { "Tailor", new[]{ "clothing", "alterations", "design" } },
    { "Blacksmith", new[]{ "metalwork", "craft", "tools" } },
    { "Butcher", new[]{ "meat", "preparation", "food" } },

    { "Graphic Designer", new[]{ "art", "design", "visuals" } },
    { "Musician", new[]{ "music", "performance", "composition" } },
    { "Writer", new[]{ "writing", "content", "creativity" } },
    { "Photographer", new[]{ "photography", "visuals", "art" } },
    { "Actor", new[]{ "performance", "theater", "film" } },
    { "Film Director", new[]{ "film", "production", "direction" } },
    { "Dancer", new[]{ "performance", "choreography", "art" } },
    { "Fashion Designer", new[]{ "fashion", "design", "clothing" } },
    { "Animator", new[]{ "animation", "digital art", "visuals" } },
    { "Illustrator", new[]{ "art", "drawing", "visuals" } },
    { "Painter (art)", new[]{ "art", "painting", "creativity" } },
    { "Makeup Artist", new[]{ "fashion", "makeup", "art" } },
    { "Jewelry Designer", new[]{ "design", "craft", "fashion" } },
    { "Interior Designer", new[]{ "design", "spaces", "decoration" } },
    { "Sculptor", new[]{ "art", "sculpture", "creativity" } },
    { "Architect", new[]{ "design", "construction", "planning" } },
    { "Industrial Designer", new[]{ "design", "products", "innovation" } },
    { "Copywriter", new[]{ "writing", "marketing", "content" } },
    { "Content Creator", new[]{ "digital", "media", "creative" } },
    { "Social Media Manager", new[]{ "social media", "marketing", "management" } },
    { "Editor", new[]{ "editing", "writing", "content" } },
    { "Journalist", new[]{ "news", "reporting", "research" } },
    { "News Anchor", new[]{ "news", "presentation", "media" } },
    { "Radio DJ", new[]{ "music", "radio", "entertainment" } },
    { "TV Producer", new[]{ "production", "media", "management" } },

    { "Police Officer", new[]{ "law", "safety", "enforcement" } },
    { "Firefighter", new[]{ "emergency", "rescue", "safety" } },
    { "Soldier", new[]{ "military", "defense", "security" } },
    { "Government Worker", new[]{ "government", "service", "public" } },
    { "Customs Officer", new[]{ "borders", "security", "inspection" } },
    { "Postal Worker", new[]{ "mail", "delivery", "logistics" } },
    { "Politician", new[]{ "leadership", "law", "public service" } },
    { "Traffic Enforcer", new[]{ "traffic", "safety", "law" } },
    { "Barangay Captain", new[]{ "leadership", "community", "governance" } },
    { "Security Guard", new[]{ "protection", "security", "safety" } },
    { "Bodyguard", new[]{ "protection", "security", "personal" } },
    { "Safety Inspector", new[]{ "safety", "inspection", "compliance" } },
    { "Forensic Investigator", new[]{ "investigation", "crime", "evidence" } },
    { "Crime Scene Analyst", new[]{ "crime", "evidence", "analysis" } },
    { "Detective", new[]{ "investigation", "crime", "law" } },
    { "Customs Security Officer", new[]{ "security", "borders", "inspection" } },
    { "Prison Guard", new[]{ "security", "prison", "protection" } },
    { "Cybersecurity Specialist", new[]{ "cybersecurity", "security", "technology" } },
    { "Lifeguard", new[]{ "rescue", "safety", "water" } }
    };

    static List<(string prompt, string[] tags)> questions = new List<(string, string[])>
{
    ("1. Are you passionate about diagnosing illnesses and creating treatment plans?", new[]{ "healthcare", "diagnosis", "treatment" }),
    ("2. Do you enjoy helping patients in hospitals or medical care?", new[]{ "patient care", "hospital", "support" }),
    ("3. Are you interested in dental health and oral care?", new[]{ "oral health", "prevention" }),
    ("4. Do you like dispensing medicines and ensuring safety?", new[]{ "medicine", "pharmacist", "safety" }),
    ("5. Do you thrive in emergency or high-pressure situations?", new[]{ "surgery", "emergency", "first aid" }),
    ("6. Do you enjoy performing lab tests or analyzing results?", new[]{ "lab", "analysis", "diagnosis" }),
    ("7. Are you focused on rehabilitation and helping patients recover mobility?", new[]{ "rehabilitation", "mobility" }),
    ("8. Do you enjoy teaching students in classrooms or schools?", new[]{ "lesson", "curriculum", "guidance" }),
    ("9. Are you interested in supporting students one-on-one?", new[]{ "support", "lesson", "tutoring" }),
    ("10. Do you like managing or organizing information and resources?", new[]{ "organization", "resources", "information" }),
    ("11. Are you interested in computer programming and building software?", new[]{ "programming", "software", "development" }),
    ("12. Do you like maintaining and troubleshooting IT systems?", new[]{ "support", "technical", "network" }),
    ("13. Do you enjoy analyzing data and finding insights?", new[]{ "data", "analysis", "reporting" }),
    ("14. Do you like designing websites or working on user interfaces?", new[]{ "web", "design", "UI" }),
    ("15. Are you interested in managing networks or IT infrastructure?", new[]{ "network", "maintenance", "management" }),
    ("16. Do you enjoy creating video games or working on gaming projects?", new[]{ "game", "design", "coding" }),
    ("17. Are you interested in artificial intelligence and machine learning?", new[]{ "AI", "systems", "technology" }),
    ("18. Do you like leading or managing teams in projects or business?", new[]{ "leadership", "management", "planning" }),
    ("19. Do you enjoy analyzing finances or auditing accounts?", new[]{ "finance", "auditing", "analysis" }),
    ("20. Are you focused on marketing, branding, or promotion?", new[]{ "marketing", "promotion", "strategy" }),
    ("21. Do you enjoy building, repairing, or creating structures or furniture?", new[]{ "construction", "woodwork", "repair" }),
    ("22. Are you interested in working with electricity or electrical systems?", new[]{ "wiring", "installation", "repair" }),
    ("23. Do you like creative work like drawing, music, or visual design?", new[]{ "creativity", "art", "design" }),
    ("24. Do you enjoy protecting people or public safety?", new[]{ "law", "safety", "police" }),
    ("25. Are you interested in science, research, or engineering?", new[]{ "research", "engineering", "science" })
};

    static Dictionary<string, List<string>> categories = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
{
    { "Health", new List<string>{ "Doctor","Nurse","Dentist","Pharmacist","Surgeon","Medical Technologist","Radiologist","Physical Therapist","Midwife","Paramedic","Veterinarian","Public Health Officer" } },
    { "Education", new List<string>{ "Teacher","Professor","Tutor","Librarian","School Principal","Guidance Counselor","Teaching Assistant","Curriculum Developer","Special Education Teacher","Researcher" } },
    { "Technology", new List<string>{ "Software Developer","IT Support Specialist","Data Analyst","Web Designer","Network Administrator","Cybersecurity Analyst","Database Administrator","Cloud Engineer","Game Developer","AI Specialist","Software Engineer","Computer Engineer" } },
    { "Business", new List<string>{ "Accountant","Manager","Banker","HR Officer","Marketing Specialist","Business Analyst","Sales Executive","Entrepreneur","Project Manager","Auditor","Financial Analyst","Stockbroker","Investment Consultant","Tax Advisor","Payroll Specialist","Loan Officer","Insurance Agent","Economist" } },
    { "Trades", new List<string>{ "Carpenter","Electrician","Mechanic","Plumber","Welder","Mason","Painter","Tailor","Blacksmith","Butcher" } },
    { "Arts & Media", new List<string>{ "Graphic Designer","Musician","Writer","Photographer","Actor","Film Director","Dancer","Fashion Designer","Painter","Animator","Journalist","News Anchor","Editor","Social Media Manager","Radio DJ","TV Producer","Copywriter","Public Relations Officer","Content Creator","Illustrator","Sculptor","Calligrapher","Makeup Artist","Jewelry Designer","Architect","Industrial Designer","Interior Designer" } },
    { "Public Service", new List<string>{ "Police Officer","Firefighter","Lawyer","Judge","Paralegal","Legal Assistant","Prosecutor","Defense Attorney","Court Clerk","Notary Public","Compliance Officer","Government Worker","Customs Officer","Postal Worker","Traffic Enforcer","Barangay Captain (local leader)" } },
    { "Engineering & Science", new List<string>{ "Civil Engineer","Environmental Scientist","Mechanical Engineer","Electrical Engineer","Chemical Engineer","Aerospace Engineer","Biomedical Engineer","Agricultural Engineer","Biologist","Chemist","Physicist","Research Scientist","Astronomer","Archaeologist","Anthropologist","Geologist","Marine Biologist","Lab Technician" } },
    { "Hospitality", new List<string>{ "Chef","Hotel Manager","Tour Guide","Flight Attendant","Travel Agent","Cruise Ship Staff","Bartender","Housekeeper","Concierge","Resort Manager" } },
    { "Agriculture", new List<string>{ "Farmer","Agricultural Engineer","Horticulturist","Fisherman","Agribusiness Manager","Soil Scientist","Forestry Worker" } },
    { "Sports", new List<string>{ "Athlete","Coach","Fitness Trainer","Sports Official","Gym Instructor","Referee","Physical Therapist (sports)","Sports Commentator","Athletic Scout","Nutritionist" } },
    { "Transport & Logistics", new List<string>{ "Pilot","Driver","Ship Captain","Train Operator","Truck Driver","Bus Conductor","Air Traffic Controller","Seafarer","Delivery Rider","Logistics Coordinator","Warehouse Worker","Delivery Driver","Supply Chain Analyst","Inventory Clerk","Forklift Operator","Procurement Specialist","Shipping Coordinator","Freight Forwarder","Distribution Manager" } },
    { "Legal & Finance", new List<string>{ "Lawyer","Judge","Paralegal","Legal Assistant","Prosecutor","Defense Attorney","Court Clerk","Notary Public","Legal Researcher","Compliance Officer","Financial Analyst","Auditor","Stockbroker","Insurance Agent","Loan Officer","Banker","Investment Consultant","Tax Advisor","Payroll Specialist","Economist" } },
    { "Social & Community", new List<string>{ "Social Worker","Daycare Worker","Counselor","Psychologist","Therapist","Caregiver","Youth Worker","Speech Therapist","Rehabilitation Specialist","Community Worker","Security Guard","Bodyguard","Safety Inspector","Forensic Investigator","Crime Scene Analyst","Detective","Customs Security Officer","Prison Guard","Cybersecurity Specialist","Lifeguard" } },
    { "Fashion & Design", new List<string>{ "Fashion Designer","Interior Designer","Animator","Sculptor","Illustrator","Architect","Industrial Designer","Calligrapher","Makeup Artist","Jewelry Designer" } }
};


    static void Main()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                PrintBanner();
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose: ");
                string choice = (Console.ReadLine() ?? "").Trim();

                if (choice.Equals("1")) RegisterUser();
                else if (choice.Equals("2"))
                {
                    string username = LoginUser();
                    if (!string.IsNullOrEmpty(username))
                        UserFlow(username);
                }
                else if (choice.Equals("3")) break;
                else
                {
                    Console.WriteLine("Invalid choice. Press Enter to continue.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
                Console.ReadLine();
            }
        }
    }

    static void PrintBanner()
    {
        Console.WriteLine(" _____  ____  ____   ___        __ __   ___   __ __  ____       _____  __ __  ______  __ __  ____     ___ ");
        Console.WriteLine("|     ||    ||    \\ |   \\      |  |  | /   \\ |  |  ||    \\     |     ||  |  ||      ||  |  ||    \\   /  _]");
        Console.WriteLine("|   __| |  | |  _  ||    \\     |  |  ||     ||  |  ||  D  )    |   __||  |  ||      ||  |  ||  D  ) /  [_ ");
        Console.WriteLine("|  |_   |  | |  |  ||  D  |    |  ~  ||  O  ||  |  ||    /     |  |_  |  |  ||_|  |_||  |  ||    / |    _]");
        Console.WriteLine("|   _]  |  | |  |  ||     |    |___, ||     ||  :  ||    \\     |   _] |  :  |  |  |  |  :  ||    \\ |   [_ ");
        Console.WriteLine("|  |    |  | |  |  ||     |    |     ||     ||     ||  .  \\    |  |   |     |  |  |  |     ||  .  \\|     |");
        Console.WriteLine("|__|   |____||__|__||_____|    |____/  \\___/  \\__,_||__|\\_|    |__|    \\__,_|  |__|   \\__,_||__|\\_||_____|");
        Console.WriteLine("                                                                                                           ");


    }

    // REGISTRATION
    static void RegisterUser()
    {
        EnsureFileExists(applicantsFile);
        while (true)
        {
            Console.Write("Enter username: ");
            string user = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(user)) { Console.WriteLine("Username cannot be empty."); continue; }
            if (IsUserExists(user))
            {
                Console.WriteLine("Username already exists. Try again.");
                continue;
            }

            Console.Write("Enter password: ");
            string pass = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(pass)) { Console.WriteLine("Password cannot be empty."); continue; }

            File.AppendAllText(applicantsFile, $"{user}:{pass}{Environment.NewLine}");
            Console.WriteLine("Registration successful! Press Enter.");
            Console.ReadLine();
            break;
        }
    }

    static bool IsUserExists(string username)
    {
        EnsureFileExists(applicantsFile);
        return File.ReadAllLines(applicantsFile).Any(x => x.Split(':')[0].Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    // LOGIN
    static string LoginUser()
    {
        EnsureFileExists(applicantsFile);
        while (true)
        {
            Console.Write("Enter username: ");
            string user = (Console.ReadLine() ?? "").Trim();
            Console.Write("Enter password: ");
            string pass = (Console.ReadLine() ?? "").Trim();

            if (File.ReadAllLines(applicantsFile).Any(x =>
                x.Split(':')[0].Equals(user, StringComparison.OrdinalIgnoreCase) &&
                x.Split(':')[1] == pass))
            {
                Console.WriteLine("Login successful! Press Enter.");
                Console.ReadLine();
                return user;
            }
            else
            {
                Console.WriteLine("Incorrect username or password. Try again.");
            }
        }
    }

    static void UserFlow(string username)
    {
        Console.Clear();
        PrintBanner();

        Console.WriteLine("Please enter your basic information.");

// Full Name
string name;
while (true)
{
    Console.Write("Full Name: ");
    name = (Console.ReadLine() ?? "").Trim();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name cannot be empty.");
        continue;
    }
    if (Regex.IsMatch(name, @"\d"))
    {
        Console.WriteLine("Name cannot contain numbers.");
        continue;
    }
    break;
}

// Age
int ageInt;
while (true)
{
    Console.Write("Age: ");
    string age = (Console.ReadLine() ?? "").Trim();
    if (!int.TryParse(age, out ageInt) || ageInt <= 0 || ageInt > 120)
    {
        Console.WriteLine("Please enter a valid age between 1 and 120.");
        continue;
    }
    break;
}

// Gender
string gender;
while (true)
{
    Console.Write("Sex (M/F): ");
    gender = (Console.ReadLine() ?? "").Trim().ToUpper();
    if (gender != "M" && gender != "F" && gender != "OTHER")
    {
        Console.WriteLine("Please enter M, F, or Other.");
        continue;
    }
    break;
}

        Dictionary<string, int> answers = new Dictionary<string, int>();
        Console.WriteLine("\nAnswer the following questions (Yes/No/Maybe):");
        foreach (var q in questions)
        {
            answers[q.prompt] = PromptYesNoMaybe(q.prompt);
        }

        List<string> recommendedJobs = RecommendJobs(answers);

        while (true)
        {
            Console.WriteLine("\nRecommended jobs based on your answers:");
            foreach (var job in recommendedJobs) Console.WriteLine("- " + job);

            int satisfiedAnswer = PromptYesNoMaybe("\nAre you satisfied with the recommended jobs?");
            if (satisfiedAnswer == 2) break;

            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Show all jobs");
            Console.WriteLine("2. Search job description");
            Console.WriteLine("3. Choose by category");
            Console.WriteLine("4. Retake questionnaire");
            Console.Write("Choose: ");
            string choice = (Console.ReadLine() ?? "").Trim();

            if (choice == "1")
            {
                Console.WriteLine("\nAll available jobs:");
                foreach (var job in allJobs.Keys) Console.WriteLine("- " + job);
            }
            else if (choice == "2")
            {
                Console.Write("Enter keyword to search in job descriptions: ");
                string keyword = (Console.ReadLine() ?? "").Trim().ToLower();

                var matches = allJobs.Where(j => j.Value.ToLower().Contains(keyword)).ToList();

                if (matches.Count == 0)
                {
                    Console.WriteLine("No jobs found with that keyword in the description.");
                }
                else
                {
                    Console.WriteLine("Jobs matching description:");
                    foreach (var match in matches)
                    {
                        Console.WriteLine($"- {match.Key}: {match.Value}");
                    }
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("\nCategories:");
                int i = 1;
                foreach (var cat in categories.Keys) { Console.WriteLine($"{i}. {cat}"); i++; }

                int catChoice = PromptNumber("Choose a category number", 1, categories.Count);
                string selectedCat = categories.Keys.ElementAt(catChoice - 1);
                Console.WriteLine($"\nJobs in {selectedCat}:");
                foreach (var job in categories[selectedCat]) Console.WriteLine("- " + job);
            }
            else if (choice == "4")
            {
                answers.Clear();
                foreach (var q in questions)
                    answers[q.prompt] = PromptYesNoMaybe(q.prompt);
                recommendedJobs = RecommendJobs(answers);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        SaveToFileAndShow(desktopPath, name, ageInt.ToString(), gender, answers, recommendedJobs);
        Console.WriteLine("\nPress Enter to log out.");
        Console.ReadLine();
    }

    static string PromptInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + ": ");
            string input = (Console.ReadLine() ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            Console.WriteLine("Input cannot be empty.");
        }
    }

    static int PromptYesNoMaybe(string prompt)
{
    while (true)
    {
        Console.Write(prompt + " (2 = Yes, 1 = Maybe, 0 = No): ");
        string input = (Console.ReadLine() ?? "").Trim();
        if (int.TryParse(input, out int value) && value >= 0 && value <= 2) 
            return value;
        Console.WriteLine("Please enter 2, 1, or 0.");
    }
}

    static int PromptNumber(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt + ": ");
            string input = (Console.ReadLine() ?? "").Trim();
            if (int.TryParse(input, out int n) && n >= min && n <= max) return n;
            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }

    static List<string> RecommendJobs(Dictionary<string, int> answers)
    {
        var recommended = new List<string>();
        foreach (var job in jobTags)
        {
            int score = job.Value.Sum(tag => answers.Sum(a =>
                a.Key.ToLower().Contains(tag.ToLower()) ? a.Value : 0
            ));
            if (score > 0) recommended.Add(job.Key);
        }
        return recommended.Count > 0 ? recommended : allJobs.Keys.ToList();
    }

    static void SaveToFileAndShow(string path, string name, string age, string gender, Dictionary<string, int> answers, List<string> jobs)
    {
        string fileName = Regex.Replace(name, @"[^\w\d]", "_") + ".txt";
        string filePath = Path.Combine(path, fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Name: " + name);
            writer.WriteLine("Age: " + age);
            writer.WriteLine("Gender: " + gender);
            writer.WriteLine("\nAnswers:");
            foreach (var a in answers)
                writer.WriteLine(a.Key + ": " + (a.Value == 2 ? "Yes" : a.Value == 1 ? "Maybe" : "No"));

            writer.WriteLine("\nRecommended Jobs:");
            foreach (var job in jobs)
                writer.WriteLine("- " + job);
        }

        Console.WriteLine($"\nInformation saved to {filePath}");
        Console.WriteLine("\n=== Summary ===");
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
        Console.WriteLine("Gender: " + gender);
        Console.WriteLine("\nAnswers:");
        foreach (var a in answers)
            Console.WriteLine(a.Key + ": " + (a.Value == 2 ? "Yes" : a.Value == 1 ? "Maybe" : "No"));
        Console.WriteLine("\nRecommended Jobs:");
        foreach (var job in jobs)
            Console.WriteLine("- " + job);
    }

    static void EnsureFileExists(string path)
    {
        if (!File.Exists(path)) File.Create(path).Close();
    }
}
