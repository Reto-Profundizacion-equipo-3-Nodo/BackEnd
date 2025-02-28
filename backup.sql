-- Table: Institutions
CREATE TABLE public.institutions (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	ubication text,
    url_generalidades text,
    url_oferta_academica text,
    url_bienestar text,
    url_admision text
);

-- Table: Categories
CREATE TABLE public.categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	description text
);

-- Table: Bootcamps
CREATE TABLE public.bootcamps (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	description text,
    information text,
    costs text,
    institution_id INT,
    FOREIGN KEY (institution_id) REFERENCES public.institutions(id) ON DELETE SET NULL
);

-- Table: Topics
CREATE TABLE public.topics (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

-- Table: Bootcamps-Topics (Many-to-Many)
CREATE TABLE public.bootcamp_topics (
    id SERIAL PRIMARY KEY,
    bootcamp_id INT NOT NULL,
    topic_id INT NOT NULL,
    FOREIGN KEY (bootcamp_id) REFERENCES public.bootcamps(id) ON DELETE CASCADE,
    FOREIGN KEY (topic_id) REFERENCES public.topics(id) ON DELETE CASCADE
);

-- Table: Opportunities
CREATE TABLE public.opportunities (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	observation text,
    type character varying(50),
    description text,
    requires text,
    guide text,
    adicional_dates text,
    service_channels text,
    manager character varying(255),
    modality character varying(50),
    category_id INT,
    institution_id INT,
    FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE SET NULL,
    FOREIGN KEY (institution_id) REFERENCES public.institutions(id) ON DELETE SET NULL
);

-- Table: Users
CREATE TABLE public.users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL, -- Remember to store encrypted passwords
    rol character varying(50) NOT NULL
);

-- Table: Users-Opportunities (Many-to-Many)
CREATE TABLE public.user_opportunities (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    opportunity_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE,
    FOREIGN KEY (opportunity_id) REFERENCES public.opportunities(id) ON DELETE CASCADE
);
