# Laqeet

## Egyptian Lost & Found Platform

Laqeet is a digital platform designed to help people in Egypt find their lost belongings and return found items to their rightful owners in a safer, faster, and more organized way.

The idea is simple: when someone loses something, they can post a report with details about the missing item. When someone finds an item, they can also post it on the platform. Laqeet connects both sides through search, filtering, ownership claims, verification questions, and secure communication.

## Problem

In Egypt, lost items are usually reported through informal channels such as Facebook groups, WhatsApp messages, posters, or word of mouth. These methods are scattered, hard to search, and often unsafe.

Common issues include:

- Lost item posts get buried quickly.
- Found items may never reach the right owner.
- People do not have one trusted place to search.
- Communication between finder and owner can be unorganized.
- There is no clear verification process to prove ownership.
- Sensitive items such as phones, wallets, IDs, keys, and documents need safer handling.

Laqeet aims to solve these problems by creating one organized Egyptian platform for lost and found items.

## Solution

Laqeet provides a simple and trusted flow:

1. A user reports a lost item or a found item.
2. The report includes category, city, location, date, description, image, and contact details.
3. Other users can browse and filter reports.
4. If someone believes an item belongs to them, they can submit a claim.
5. For found items, the finder can add verification questions that only the real owner should know.
6. If the claimant answers correctly, a verified conversation opens between both users.
7. The two users can safely arrange the return of the item.

The platform is focused on trust, clarity, and ease of use.

## Target Users

Laqeet is designed for:

- People who lost personal belongings.
- People who found items and want to return them.
- Students in universities and schools.
- Public transport users.
- Malls, cafes, gyms, offices, and public places.
- Admins or moderators who manage reports and claims.

## Supported Item Types

The platform can support many categories, including:

- Wallets
- Phones
- Keys
- Bags
- Accessories
- Watches
- Electronics
- Documents and IDs
- Pets
- Other personal items

## Core Features

### Lost and Found Reports

Users can create reports for either:

- Lost items
- Found items

Each report contains item details, city, location, date, image, category, and contact information.

### Browse and Search

Users can search and filter reports by:

- Keyword
- Status
- City
- Category
- Date

This makes it easier to find relevant reports instead of searching through random social media posts.

### Claim System

When a user finds a report that may match their lost item, they can submit a claim.

For found items, the claim is connected to verification questions. This helps reduce fake claims and gives the finder more confidence before communicating with the claimant.

### Verification Questions

The finder can add questions such as:

- What is inside the wallet?
- What color is the phone case?
- How many keys are in the keychain?
- What name appears on the document?

Only the real owner is expected to know the correct answers.

### Verified Chat

The chat is not a public messaging feature.

It opens only after a claim passes verification. This means the finder and the claimant already have a level of trust before they start talking.

The goal is to make communication safer and more focused.

### User Profile

Users can view their reports, claims, contact information, and activity from their profile page.

### Admin Dashboard

The admin dashboard helps moderators track:

- Total reports
- Lost reports
- Found reports
- Active claims
- Recent reports
- Category distribution

This can later support moderation, suspicious report review, and platform analytics.

## Business Value

Laqeet can become a centralized lost-and-found platform for Egypt.

Potential value includes:

- Helping individuals recover important belongings.
- Reducing chaos caused by scattered posts across social platforms.
- Supporting public places with a better lost-and-found process.
- Building trust between finders and owners.
- Creating a scalable system that can later support partnerships with malls, universities, cafes, transport stations, and organizations.

## Why Egypt?

Egypt has high daily movement in public transportation, universities, malls, workplaces, cafes, and crowded streets. Losing personal belongings is common, but the recovery process is usually random and unstructured.

Laqeet is designed around this local need. It uses Arabic RTL UI, Egyptian cities, familiar item categories, and a simple user experience suitable for everyday users.

## Current Project Status

This repository currently contains the frontend prototype.

It includes:

- Static HTML pages
- CSS styling
- Demo JavaScript data
- Local browser storage for demo reports
- UI flows for reports, claims, verification, chat, profile, and admin

The project is prepared to be connected later with an ASP.NET MVC backend.

## Future Backend Integration

The next development phase should include:

- ASP.NET MVC controllers
- Entity Framework database models
- Real user authentication
- Role-based admin access
- Server-side report creation
- Server-side claim verification
- Secure image upload
- Real database-backed messages
- SignalR for real-time chat

## Suggested Main Modules

- Authentication module
- Reports module
- Claims module
- Verification module
- Messaging module
- Admin module
- User profile module

## Technology Used in This Prototype

- HTML
- CSS
- JavaScript
- Bootstrap
- Font Awesome
- LocalStorage for temporary demo data

## Vision

Laqeet aims to make returning lost belongings easier and more trustworthy in Egypt.

Instead of relying on scattered posts and luck, the platform gives people one organized place to report, search, verify, communicate, and return items safely.
