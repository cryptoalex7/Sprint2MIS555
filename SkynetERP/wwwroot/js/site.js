// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Modern Navigation - Active Page Highlighting
document.addEventListener('DOMContentLoaded', function() {
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.modern-nav-link');
    
    navLinks.forEach(link => {
        const linkPath = new URL(link.href).pathname.toLowerCase();
        
        // Check if current path matches the link path
        if (currentPath === linkPath || 
            (currentPath !== '/' && linkPath !== '/' && currentPath.includes(linkPath.replace('/', '')))) {
            link.classList.add('active');
            link.setAttribute('aria-current', 'page');
        }
    });
});